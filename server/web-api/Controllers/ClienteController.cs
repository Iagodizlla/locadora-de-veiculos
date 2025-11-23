using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Excluir;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Inserir;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarClientesPJ;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarTodos;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/clientes")]
public class ClienteController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(InserirClienteResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirClienteRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarClienteResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarClientePartialRequest request)
    {
        var editarRequest = new EditarClienteRequest(
            id,
            request.Nome,
            request.Endereco,
            request.Telefone,
            request.TipoCliente,
            request.Documento,
            request.Cnh
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirClienteResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirClienteRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarClientesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarClientesRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SelecionarClientePorIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarClientePorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet("pj")]
    [ProducesResponseType(typeof(SelecionarClientesPJResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarClientesPJ()
    {
        var resultado = await mediator.Send(new SelecionarClientesPJRequest());

        return resultado.ToHttpResponse();
    }
}
