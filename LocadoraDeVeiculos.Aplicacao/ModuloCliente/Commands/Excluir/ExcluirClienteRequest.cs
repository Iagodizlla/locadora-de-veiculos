using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Excluir;

public record ExcluirClienteRequest(Guid Id) : IRequest<Result<ExcluirClienteResponse>>;