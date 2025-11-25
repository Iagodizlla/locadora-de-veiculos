using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Excluir;

public class ExcluirPlanoRequestHandler(
    IRepositorioAluguel repositorioAluguel,
    IRepositorioPlano repositorioPlano,
    IContextoPersistencia contexto
) : IRequestHandler<ExcluirPlanoRequest, Result<ExcluirPlanoResponse>>
{
    public async Task<Result<ExcluirPlanoResponse>> Handle(ExcluirPlanoRequest request, CancellationToken cancellationToken)
    {
        var planoSelecionado = await repositorioPlano.SelecionarPorIdAsync(request.Id);

        if (await repositorioAluguel.ExisteAluguelComPlanoAsync(request.Id))
            return Result.Fail(PlanoErrorResults.PlanoPossuiAlugueisError());

        if (planoSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        try
        {
            await repositorioPlano.ExcluirAsync(planoSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirPlanoResponse());
    }
}