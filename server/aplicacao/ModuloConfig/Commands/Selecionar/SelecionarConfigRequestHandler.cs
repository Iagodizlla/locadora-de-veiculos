using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloConfig;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloConfig.Commands.Selecionar;

public class SelecionarConfigRequestHandler(
    IRepositorioConfig repositorioConfig
) : IRequestHandler<SelecionarConfigRequest, Result<SelecionarConfigResponse>>
{
    public async Task<Result<SelecionarConfigResponse>> Handle(
        SelecionarConfigRequest request,
        CancellationToken cancellationToken)
    {
        var configuracao = await repositorioConfig.SelecionarAsync();

        if (configuracao == null)
            return Result.Fail(ErrorResults.NotFoundError(configuracao.Id));

        return Result.Ok(
            new SelecionarConfigResponse(
                configuracao.Gasolina,
                configuracao.Gas,
                configuracao.Diesel,
                configuracao.Alcool
            )
        );
    }
}
