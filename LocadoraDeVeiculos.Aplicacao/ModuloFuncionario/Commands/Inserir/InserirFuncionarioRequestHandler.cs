using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Inserir;

public class InserirFuncionarioRequestHandler(
    IContextoPersistencia contexto,
    IRepositorioFuncionario repositorioFuncionario,
    ITenantProvider tenantProvider,
    IValidator<Funcionario> validador
) : IRequestHandler<InserirFuncionarioRequest, Result<InserirFuncionarioResponse>>
{
    public async Task<Result<InserirFuncionarioResponse>> Handle(
        InserirFuncionarioRequest request, CancellationToken cancellationToken)
    {
        var funcionario = new Funcionario(request.Nome, request.Salario, request.Admissao)
        {
            UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault()
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
            return Result.Fail(FuncionarioErrorResults.NomeDuplicadoError(funcionario.Nome));

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
                registro.Nome,
                funcionario.Nome,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}
