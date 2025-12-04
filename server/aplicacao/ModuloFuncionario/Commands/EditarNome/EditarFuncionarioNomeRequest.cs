using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.EditarNome;

public record AutoEditarFuncionarioPartialRequest(string Username);

public record AutoEditarFuncionarioRequest(string Username)
    : IRequest<Result<AutoEditarFuncionarioResponse>>;
