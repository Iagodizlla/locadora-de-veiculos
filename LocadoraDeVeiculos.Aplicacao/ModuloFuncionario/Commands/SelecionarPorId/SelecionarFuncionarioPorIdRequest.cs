using FluentResults;
using LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;

public record SelecionarFuncionarioPorIdRequest(Guid Id) : IRequest<Result<SelecionarFuncionarioPorIdResponse>>;