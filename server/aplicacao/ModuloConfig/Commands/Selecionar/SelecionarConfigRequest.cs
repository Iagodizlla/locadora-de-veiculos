using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloConfig.Commands.Selecionar;

public record SelecionarConfigRequest()
    : IRequest<Result<SelecionarConfigResponse>>;
