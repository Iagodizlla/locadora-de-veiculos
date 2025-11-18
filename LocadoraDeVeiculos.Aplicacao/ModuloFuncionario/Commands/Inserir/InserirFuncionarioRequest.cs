using FluentResults;
using MediatR;

namespace LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Inserir;

public record InserirFuncionarioRequest(string Nome, double Salario, DateTimeOffset Admissao)
    : IRequest<Result<InserirFuncionarioResponse>>;
