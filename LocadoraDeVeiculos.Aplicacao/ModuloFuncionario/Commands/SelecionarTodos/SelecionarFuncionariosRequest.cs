using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;

public record SelecionarFuncionariosRequest : IRequest<Result<SelecionarFuncionariosResponse>>;