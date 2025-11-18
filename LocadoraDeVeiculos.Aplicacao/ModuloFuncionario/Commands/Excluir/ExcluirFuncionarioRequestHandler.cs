using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Excluir;

public class ExcluirFuncionarioRequestHandler(
    IRepositorioFuncionario repositorioFuncionario,
    IContextoPersistencia contexto
) : IRequestHandler<ExcluirFuncionarioRequest, Result<ExcluirFuncionarioResponse>>
{
    public async Task<Result<ExcluirFuncionarioResponse>> Handle(ExcluirFuncionarioRequest request, CancellationToken cancellationToken)
    {
        var medicoSelecionado = await repositorioFuncionario.SelecionarPorIdAsync(request.Id);

        if (medicoSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        try
        {
            await repositorioFuncionario.ExcluirAsync(medicoSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirFuncionarioResponse());
    }
}