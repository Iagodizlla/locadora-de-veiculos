using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarPorId;

public record SelecionarAluguelPorIdRequest(Guid Id) : IRequest<Result<SelecionarAluguelPorIdResponse>>;