using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.EditarNome;

public class AutoEditarFuncionarioRequestHandler(
    IRepositorioFuncionario repositorioFuncionario,
    IContextoPersistencia contexto,
    IValidator<Funcionario> validador,
    IContextoUsuario contextoUsuario
) : IRequestHandler<AutoEditarFuncionarioRequest, Result<AutoEditarFuncionarioResponse>>
{
    public async Task<Result<AutoEditarFuncionarioResponse>> Handle(AutoEditarFuncionarioRequest request, CancellationToken cancellationToken)
    {
        // ID do usuário autenticado (claim)
        var usuarioId = contextoUsuario.GetUserId();

        // Agora busca o funcionário pelo UsuarioId
        var funcionarioSelecionado = await repositorioFuncionario.SelecionarPorUsuarioIdAsync(usuarioId);

        if (funcionarioSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(usuarioId));

        funcionarioSelecionado.Nome = request.Nome;

        var resultadoValidacao =
            await validador.ValidateAsync(funcionarioSelecionado, cancellationToken);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(f => f.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        try
        {
            await repositorioFuncionario.EditarAsync(funcionarioSelecionado);
            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new AutoEditarFuncionarioResponse(funcionarioSelecionado.Id));
    }
}
