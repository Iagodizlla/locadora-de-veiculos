using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Excluir;

public class ExcluirGrupoAutomovelRequestHandler(
    IRepositorioAutomovel repositorioAutomovel,
    IRepositorioGrupoAutomovel repositorioGrupoAutomovel,
    IContextoPersistencia contexto
) : IRequestHandler<ExcluirGrupoAutomovelRequest, Result<ExcluirGrupoAutomovelResponse>>
{
    public async Task<Result<ExcluirGrupoAutomovelResponse>> Handle(ExcluirGrupoAutomovelRequest request, CancellationToken cancellationToken)
    {
        var grupoAutomovelSelecionado = await repositorioGrupoAutomovel.SelecionarPorIdAsync(request.Id);

        if (grupoAutomovelSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        // 🔥 REGRA DE NEGÓCIO: verificar se há automóveis vinculados
        var existeAutomovel = await repositorioAutomovel.ExisteAutomovelComGrupoAsync(request.Id);

        if (existeAutomovel)
            return Result.Fail(GrupoAutomovelErrorResults.GrupoPossuiAutomoveisError());

        try
        {
            await repositorioGrupoAutomovel.ExcluirAsync(grupoAutomovelSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirGrupoAutomovelResponse());
    }
}