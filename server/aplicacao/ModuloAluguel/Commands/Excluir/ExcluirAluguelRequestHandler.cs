using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Excluir;

public class ExcluirAluguelRequestHandler(
    IRepositorioAluguel repositorioAluguel,
    IContextoPersistencia contexto
) : IRequestHandler<ExcluirAluguelRequest, Result<ExcluirAluguelResponse>>
{
    public async Task<Result<ExcluirAluguelResponse>> Handle(ExcluirAluguelRequest request, CancellationToken cancellationToken)
    {
        var aluguelSelecionado = await repositorioAluguel.SelecionarPorIdAsync(request.Id);

        if (aluguelSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        if (aluguelSelecionado.Status == true)
            return Result.Fail(AluguelErrorResults.AluguelNaoPodeSerExcluidoError());

        try
        {
            await repositorioAluguel.ExcluirAsync(aluguelSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirAluguelResponse());
    }
}