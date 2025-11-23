using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarClientesPJ;

public record SelecionarClientesPJRequest : IRequest<Result<SelecionarClientesPJResponse>>;
