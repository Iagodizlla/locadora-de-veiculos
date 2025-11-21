using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarPorId;

public record SelecionarClientePorIdRequest(Guid Id) : IRequest<Result<SelecionarClientePorIdResponse>>;