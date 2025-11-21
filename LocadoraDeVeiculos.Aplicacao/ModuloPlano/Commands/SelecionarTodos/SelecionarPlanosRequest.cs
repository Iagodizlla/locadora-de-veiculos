using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.SelecionarTodos;

public record SelecionarPlanosRequest : IRequest<Result<SelecionarPlanosResponse>>;