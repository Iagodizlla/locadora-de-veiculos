using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Excluir;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Inserir;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.SelecionarTodos;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/grupo-automoveis")]
public class GrupoAutomovelController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(InserirGrupoAutomovelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirGrupoAutomovelRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarGrupoAutomovelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarGrupoAutomovelPartialRequest request)
    {
        var editarRequest = new EditarGrupoAutomovelRequest(
            id,
            request.Nome
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirGrupoAutomovelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirGrupoAutomovelRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarGrupoAutomoveisResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarGrupoAutomoveisRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SelecionarGrupoAutomovelPorIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarGrupoAutomovelPorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
    }
}
