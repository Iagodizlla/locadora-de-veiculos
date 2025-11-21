using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.SelecionarPorId;

public record SelecionarPlanoPorIdRequest(Guid Id) : IRequest<Result<SelecionarPlanoPorIdResponse>>;