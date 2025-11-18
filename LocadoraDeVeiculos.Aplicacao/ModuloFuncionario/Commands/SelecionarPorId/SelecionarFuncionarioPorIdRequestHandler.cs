using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;
using MediatR;

namespace OrganizaMed.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;

public class SelecionarFuncionarioPorIdRequestHandler(
    IRepositorioFuncionario repositorioFuncionario
) : IRequestHandler<SelecionarFuncionarioPorIdRequest, Result<SelecionarFuncionarioPorIdResponse>>
{
    public async Task<Result<SelecionarFuncionarioPorIdResponse>> Handle(SelecionarFuncionarioPorIdRequest request, CancellationToken cancellationToken)
    {
        var funcionarioSelecionado = await repositorioFuncionario.SelecionarPorIdAsync(request.Id);

        if (funcionarioSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        var resposta = new SelecionarFuncionarioPorIdResponse(
            funcionarioSelecionado.Id,
            funcionarioSelecionado.Nome,
            funcionarioSelecionado.Salario,
            funcionarioSelecionado.Admissao
        );

        return Result.Ok(resposta);
    }
}