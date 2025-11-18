using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Excluir;

public class ExcluirFuncionarioRequestHandler(
    IRepositorioFuncionario repositorioFuncionario,
    IContextoPersistencia contexto
) : IRequestHandler<ExcluirFuncionarioRequest, Result<ExcluirFuncionarioResponse>>
{
    public async Task<Result<ExcluirFuncionarioResponse>> Handle(ExcluirFuncionarioRequest request, CancellationToken cancellationToken)
    {
        var funcionarioSelecionado = await repositorioFuncionario.SelecionarPorIdAsync(request.Id);

        if (funcionarioSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        try
        {
            await repositorioFuncionario.ExcluirAsync(funcionarioSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirFuncionarioResponse());
    }
}