using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.EditarNome;

public record AutoEditarFuncionarioPartialRequest(string Nome);

public record AutoEditarFuncionarioRequest(string Nome)
    : IRequest<Result<AutoEditarFuncionarioResponse>>;
