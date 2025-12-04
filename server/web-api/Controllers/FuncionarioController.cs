using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.EditarNome;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Excluir;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Inserir;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Route("api/funcionarios")]
public class FuncionarioController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [Authorize(Roles = "Empresa")]
    [ProducesResponseType(typeof(InserirFuncionarioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirFuncionarioRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Empresa")]
    [ProducesResponseType(typeof(EditarFuncionarioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarFuncionarioPartialRequest request)
    {
        var editarRequest = new EditarFuncionarioRequest(
            id,
            request.UserName,
            request.Salario,
            request.Admissao
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Empresa")]
    [ProducesResponseType(typeof(ExcluirFuncionarioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirFuncionarioRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [Authorize(Roles = "Empresa")]
    [ProducesResponseType(typeof(SelecionarFuncionariosResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarFuncionariosRequest());

        return resultado.ToHttpResponse();
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Empresa")]
    [ProducesResponseType(typeof(SelecionarFuncionarioPorIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarPorId(Guid id)
    {
        var selecionarPorIdRequest = new SelecionarFuncionarioPorIdRequest(id);

        var resultado = await mediator.Send(selecionarPorIdRequest);

        return resultado.ToHttpResponse();
    }

    [HttpPut("auto-editar")]
    [Authorize(Roles = "Funcionario")]
    [ProducesResponseType(typeof(AutoEditarFuncionarioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> AutoEditar(
    [FromServices] IContextoUsuario userContext,
    [FromBody] AutoEditarFuncionarioPartialRequest partialRequest)
    {
        var editarRequest = new AutoEditarFuncionarioRequest(partialRequest.Username);

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }
}
