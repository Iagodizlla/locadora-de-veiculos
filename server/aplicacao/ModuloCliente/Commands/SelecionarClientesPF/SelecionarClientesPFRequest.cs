using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarClientesPF;

public record SelecionarClientesPFRequest : IRequest<Result<SelecionarClientesPFResponse>>;
