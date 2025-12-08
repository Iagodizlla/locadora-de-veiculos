using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Finalizar;

public record FinalizarAluguelRequest(Guid Id, DateTimeOffset DataDevolucao, int QuilometragemFinal, int NivelCombustivelNaDevolucao)
    : IRequest<Result<FinalizarAluguelResponse>>;
