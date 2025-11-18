using FluentResults;
using MediatR;

namespace LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Excluir;

public record ExcluirFuncionarioRequest(Guid Id) : IRequest<Result<ExcluirFuncionarioResponse>>;