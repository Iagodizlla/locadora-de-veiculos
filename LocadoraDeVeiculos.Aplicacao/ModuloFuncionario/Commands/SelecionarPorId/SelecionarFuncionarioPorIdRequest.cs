using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;

public record SelecionarFuncionarioPorIdRequest(Guid Id) : IRequest<Result<SelecionarFuncionarioPorIdResponse>>;