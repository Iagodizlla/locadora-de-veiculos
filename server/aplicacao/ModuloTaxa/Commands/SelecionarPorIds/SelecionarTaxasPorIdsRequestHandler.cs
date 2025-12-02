using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarPorIds;

public class SelecionarTaxasPorIdsRequestHandler(
    IRepositorioTaxa repositorioTaxa
) : IRequestHandler<SelecionarTaxasPorIdsRequest, Result<SelecionarTaxasPorIdsResponse>>
{
    public async Task<Result<SelecionarTaxasPorIdsResponse>> Handle(
        SelecionarTaxasPorIdsRequest request,
        CancellationToken cancellationToken)
    {
        var registros = await repositorioTaxa.SelecionarTodosPorIdAsync(request.Ids);

        var response = new SelecionarTaxasPorIdsResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros.Select(r =>
                new SelecionarTaxasPorIdsDto(r.Id, r.Nome, r.Preco, r.Servico))
        };

        return Result.Ok(response);
    }
}
