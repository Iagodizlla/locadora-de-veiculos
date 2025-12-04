using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Inserir;

public record InserirFuncionarioRequest(double Salario, DateTimeOffset Admissao, string UserName, string Email, string Password)
    : IRequest<Result<InserirFuncionarioResponse>>;
