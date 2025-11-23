using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarPorId;

public record SelecionarTaxaPorIdRequest(Guid Id) : IRequest<Result<SelecionarTaxaPorIdResponse>>;