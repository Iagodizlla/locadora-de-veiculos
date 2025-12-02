using LocadoraDeVeiculos.Aplicacao.ModuloConfig.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloConfig.Commands.Selecionar;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[Route("api/configuracoes")]
[ApiController]
public class ConfigController(IMediator mediator) : ControllerBase
{
    [HttpPut]
    [ProducesResponseType(typeof(EditarConfigResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(EditarConfigRequest request)
    {
        var editarRequest = new EditarConfigRequest(
            request.Gasolina,
            request.Gas,
            request.Diesel,
            request.Alcool
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarConfigResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Selecionar()
    {
        var resultado = await mediator.Send(new SelecionarConfigRequest());
        return resultado.ToHttpResponse();
    }
}
