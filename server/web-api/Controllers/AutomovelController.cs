using LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.Excluir;
using LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.Inserir;
using LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarPorGrupo;
using LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarTodos;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/automoveis")]
public class AutomovelController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(InserirAutomovelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirAutomovelRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarAutomovelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarAutomovelPartialRequest request)
    {
        var editarRequest = new EditarAutomovelRequest(
            id,
            request.Placa,
            request.Modelo,
            request.Marca,
            request.Cor,
            request.Ano,
            request.CapacidadeTanque,
            request.Foto,
            request.GrupoAutomovel,
            request.Combustivel
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirAutomovelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirAutomovelRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarAutomoveisResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarAutomoveisRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SelecionarAutomovelPorIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarAutomovelPorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet("grupo/{grupoId:guid}")]
    [ProducesResponseType(typeof(SelecionarAutomoveisPorGrupoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorGrupo(Guid grupoId)
    {
        var request = new SelecionarAutomoveisPorGrupoRequest(grupoId);

        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }
}
