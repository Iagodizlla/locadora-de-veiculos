using LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Excluir;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Inserir;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarClientes;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarNaoClientes;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarTodos;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/condutores")]
public class CondutorController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(InserirCondutorResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirCondutorRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarCondutorResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarCondutorPartialRequest request)
    {
        var editarRequest = new EditarCondutorRequest(
            id,
            request.Nome,
            request.Cnh,
            request.Cpf,
            request.Telefone,
            request.Categoria,
            request.ValidadeCnh,
            request.ECliente
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirCondutorResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirCondutorRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarCondutoresResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarCondutoresRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SelecionarCondutorPorIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarCondutorPorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet("nao-clientes")]
    [ProducesResponseType(typeof(SelecionarCondutoresNaoClientesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarNaoClientes()
    {
        var resultado = await mediator.Send(new SelecionarCondutoresNaoClientesRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("clientes")]
    [ProducesResponseType(typeof(SelecionarCondutoresClientesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarClientes()
    {
        var resultado = await mediator.Send(new SelecionarCondutoresClientesRequest());

        return resultado.ToHttpResponse();
    }
}
