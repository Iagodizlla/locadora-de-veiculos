using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Excluir;

public record ExcluirAluguelRequest(Guid Id) : IRequest<Result<ExcluirAluguelResponse>>;