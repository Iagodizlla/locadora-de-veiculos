
using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloConfig.Commands.Editar;

public record EditarConfigRequest(
    decimal Gasolina,
    decimal Gas,
    decimal Diesel,
    decimal Alcool
) : IRequest<Result<EditarConfigResponse>>;
