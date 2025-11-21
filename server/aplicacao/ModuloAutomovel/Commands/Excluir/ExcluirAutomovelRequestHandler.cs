using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.Excluir;

public class ExcluirAutomovelRequestHandler(
    IRepositorioAutomovel repositorioAutomovel,
    IContextoPersistencia contexto
) : IRequestHandler<ExcluirAutomovelRequest, Result<ExcluirAutomovelResponse>>
{
    public async Task<Result<ExcluirAutomovelResponse>> Handle(ExcluirAutomovelRequest request, CancellationToken cancellationToken)
    {
        var automovelSelecionado = await repositorioAutomovel.SelecionarPorIdAsync(request.Id);

        if (automovelSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        try
        {
            await repositorioAutomovel.ExcluirAsync(automovelSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirAutomovelResponse());
    }
}