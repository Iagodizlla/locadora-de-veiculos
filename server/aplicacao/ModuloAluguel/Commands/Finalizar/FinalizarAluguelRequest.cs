using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Finalizar;

public record FinalizarAluguelRequest(Guid Id, DateTimeOffset? DataDevolucao, int? KmFinal, int? NivelCombustivelNaDevolucao)
    : IRequest<Result<FinalizarAluguelResponse>>;
