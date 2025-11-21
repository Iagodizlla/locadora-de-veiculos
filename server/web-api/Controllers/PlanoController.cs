using LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Excluir;
using LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Inserir;
using LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.SelecionarTodos;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/planos")]
public class PlanoController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(InserirPlanoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirPlanoRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarPlanoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarPlanoPartialRequest request)
    {
        var editarRequest = new EditarPlanoRequest(
            id,
            request.TipoPlano,
            request.GrupoAutomovelId,
            request.PrecoDiario,
            request.PrecoPorKm,
            request.KmLivres,
            request.PrecoporKmExplorado,
            request.PrecoLivre
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirPlanoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirPlanoRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarPlanosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarPlanosRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SelecionarPlanoPorIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarPlanoPorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
    }
}
