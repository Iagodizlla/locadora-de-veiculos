using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.CalcularValor;

public record CalcularValorAluguelRequest(Guid Id) : IRequest<Result<CalcularValorAluguelResponse>>;