using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarPorIds;

public record SelecionarTaxasPorIdsRequest(List<Guid> Ids)
    : IRequest<Result<SelecionarTaxasPorIdsResponse>>;
