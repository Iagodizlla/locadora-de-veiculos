using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Excluir;

public record ExcluirFuncionarioRequest(Guid Id) : IRequest<Result<ExcluirFuncionarioResponse>>;