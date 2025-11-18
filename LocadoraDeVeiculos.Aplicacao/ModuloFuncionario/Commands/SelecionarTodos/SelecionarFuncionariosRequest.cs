using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;
using MediatR;

namespace LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;

public record SelecionarFuncionariosRequest : IRequest<Result<SelecionarFuncionariosResponse>>;