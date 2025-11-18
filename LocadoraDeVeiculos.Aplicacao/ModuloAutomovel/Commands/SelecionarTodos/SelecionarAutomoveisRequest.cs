using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarTodos;

public record SelecionarAutomoveisRequest : IRequest<Result<SelecionarAutomoveisResponse>>;