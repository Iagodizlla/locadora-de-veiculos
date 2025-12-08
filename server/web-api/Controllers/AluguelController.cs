using LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Excluir;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Finalizar;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Inserir;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarAtivos;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarFinalizados;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarTodos;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/alugueis")]
public class AluguelController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(InserirAluguelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirAluguelRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarAluguelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarAluguelPartialRequest request)
    {
        var editarRequest = new EditarAluguelRequest(
            id,
            request.ClienteId,
            request.CondutorId,
            request.AutomovelId,
            request.PlanoId,
            request.TaxasId,
            request.DataSaisa,
            request.DataRetornoPrevista,
            request.DataDevolucao,
            request.KmInicial,
            request.KmFinal,
            request.NivelCombustivelNaSaida,
            request.NivelCombustivelNaDevolucao,
            request.SeguroCliente,
            request.SeguroTerceiro,
            request.ValorSeguroPorDia,
            request.Status,
            request.valorTotal
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirAluguelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirAluguelRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarAlugueisResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarAlugueisRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SelecionarAluguelPorIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarAluguelPorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
    }

    [HttpPut("finalizar/{id:guid}")]
    [ProducesResponseType(typeof(FinalizarAluguelResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Finalizar(Guid id, FinalizarAluguelRequest request)
    {
        var finalizarRequest = new FinalizarAluguelRequest(id, request.DataDevolucao, request.QuilometragemFinal, request.NivelCombustivelNaDevolucao);

        var resultado = await mediator.Send(finalizarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet("ativos")]
    [ProducesResponseType(typeof(SelecionarAlugueisAtivosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarAtivos()
    {
        var resultado = await mediator.Send(new SelecionarAlugueisAtivosRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("finalizados")]
    [ProducesResponseType(typeof(SelecionarAlugueisFinalizadosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarFinalizados()
    {
        var resultado = await mediator.Send(new SelecionarAlugueisFinalizadosRequest());

        return resultado.ToHttpResponse();
    }
}
