using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Inserir;

public class InserirFuncionarioRequestHandler(
    UserManager<Usuario> userManager,
    IContextoPersistencia contexto,
    IRepositorioFuncionario repositorioFuncionario,
    ITenantProvider tenantProvider,
    IValidator<Funcionario> validador
) : IRequestHandler<InserirFuncionarioRequest, Result<InserirFuncionarioResponse>>
{
    public async Task<Result<InserirFuncionarioResponse>> Handle(
        InserirFuncionarioRequest request, CancellationToken cancellationToken)
    {
        var usuario = new Usuario
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var usuarioResult = await userManager.CreateAsync(usuario, request.Password);

        if (!usuarioResult.Succeeded)
        {
            var erros = usuarioResult
                .Errors
                .Select(failure => failure.Description)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        usuario.AssociarEmpresa(tenantProvider.EmpresaId.GetValueOrDefault());

        await userManager.AddToRoleAsync(usuario, "Funcionario");

        var funcionario = new Funcionario(request.UserName, request.Salario, request.Admissao)
        {
            EmpresaId = tenantProvider.EmpresaId.GetValueOrDefault(),
            UsuarioId = usuario.Id
        };

        // validações
        var resultadoValidacao = await validador.ValidateAsync(funcionario);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
               .Select(failure => failure.ErrorMessage)
               .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var funcionariosRegistrados = await repositorioFuncionario.SelecionarTodosAsync();

        if (NomeDuplicado(funcionario, funcionariosRegistrados))
            return Result.Fail(FuncionarioErrorResults.NomeDuplicadoError(funcionario.Usuario.UserName));

        // inserção
        try
        {
            await repositorioFuncionario.InserirAsync(funcionario);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new InserirFuncionarioResponse(funcionario.Id));
    }

    private bool NomeDuplicado(Funcionario funcionario, IList<Funcionario> funcionarios)
    {
        return funcionarios
            .Any(registro => string.Equals(
                registro.Usuario.UserName,
                funcionario.Usuario.UserName,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}
