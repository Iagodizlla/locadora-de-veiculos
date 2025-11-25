using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Excluir;

public class ExcluirTaxaRequestHandler(
    IRepositorioAluguel repositorioAluguel,
    IRepositorioTaxa repositorioTaxa,
    IContextoPersistencia contexto
) : IRequestHandler<ExcluirTaxaRequest, Result<ExcluirTaxaResponse>>
{
    public async Task<Result<ExcluirTaxaResponse>> Handle(ExcluirTaxaRequest request, CancellationToken cancellationToken)
    {
        var taxaSelecionado = await repositorioTaxa.SelecionarPorIdAsync(request.Id);

        if (taxaSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        if (await repositorioAluguel.TaxaEmAluguelAtivoAsync(request.Id))
            return Result.Fail(TaxaErrorResults.TaxaComAluguelNaoFinalizadoError());

        try
        {
            await repositorioTaxa.ExcluirAsync(taxaSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirTaxaResponse());
    }
}