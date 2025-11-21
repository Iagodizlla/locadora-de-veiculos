using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarTodos;

public record SelecionarCondutoresRequest : IRequest<Result<SelecionarCondutoresResponse>>;