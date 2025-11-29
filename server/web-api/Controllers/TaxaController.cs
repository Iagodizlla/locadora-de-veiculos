using LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Excluir;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Inserir;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarPorIds;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarTodos;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/taxas")]
public class TaxaController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(InserirTaxaResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirTaxaRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarTaxaResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarTaxaPartialRequest request)
    {
        var editarRequest = new EditarTaxaRequest(
            id,
            request.Nome,
            request.Preco,
            request.Servico,
            request.AlugueisId
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirTaxaResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirTaxaRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarTaxasResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarTaxasRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SelecionarTaxaPorIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarTaxaPorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
    }

    [HttpPost("por-ids")]
    [ProducesResponseType(typeof(SelecionarTaxasPorIdsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorIds([FromBody] List<Guid> ids)
    {
        var request = new SelecionarTaxasPorIdsRequest(ids);

        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }
}