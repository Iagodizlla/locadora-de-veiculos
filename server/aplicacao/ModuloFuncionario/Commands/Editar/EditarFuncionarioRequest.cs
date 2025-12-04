using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Editar;

public record EditarFuncionarioPartialRequest(string UserName, double Salario, DateTimeOffset Admissao);

public record EditarFuncionarioRequest(Guid Id, string Username, double Salario, DateTimeOffset Admissao)
    : IRequest<Result<EditarFuncionarioResponse>>;