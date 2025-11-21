using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;

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