using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarTodos;

public record SelecionarAlugueisRequest : IRequest<Result<SelecionarAlugueisResponse>>;