using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Finalizar;

public record FinalizarAluguelRequest(Guid Id)
    : IRequest<Result<FinalizarAluguelResponse>>;
