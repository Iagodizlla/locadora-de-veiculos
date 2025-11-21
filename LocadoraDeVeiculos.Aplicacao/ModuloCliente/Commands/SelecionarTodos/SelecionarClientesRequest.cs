using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarTodos;

public record SelecionarClientesRequest : IRequest<Result<SelecionarClientesResponse>>;