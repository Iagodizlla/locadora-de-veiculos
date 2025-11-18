using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;
using LocadoraDeVeiculos.WebApi.Extensions;
using LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Editar;
using LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Excluir;
using LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Inserir;
using LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;
using LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocadoreDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/funcionarios")]
public class FuncionarioController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(InserirFuncionarioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirFuncionarioRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarFuncionarioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarFuncionarioPartialRequest request)
    {
        var editarRequest = new EditarFuncionarioRequest(
            id,
            request.Nome,
            request.Salario,
            request.Admissao
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirFuncionarioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirFuncionarioRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarFuncionariosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarFuncionariosRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SelecionarFuncionarioPorIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarFuncionarioPorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
    }
}
