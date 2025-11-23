using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Excluir;

public record ExcluirTaxaRequest(Guid Id) : IRequest<Result<ExcluirTaxaResponse>>;