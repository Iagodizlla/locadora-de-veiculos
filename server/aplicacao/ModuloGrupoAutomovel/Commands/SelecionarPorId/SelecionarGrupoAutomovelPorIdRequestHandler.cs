using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.SelecionarPorId;

public class SelecionarGrupoAutomovelPorIdRequestHandler(
    IRepositorioGrupoAutomovel repositorioGrupoAutomovel
) : IRequestHandler<SelecionarGrupoAutomovelPorIdRequest, Result<SelecionarGrupoAutomovelPorIdResponse>>
{
    public async Task<Result<SelecionarGrupoAutomovelPorIdResponse>> Handle(SelecionarGrupoAutomovelPorIdRequest request, CancellationToken cancellationToken)
    {
        var grupoAutomovelSelecionado = await repositorioGrupoAutomovel.SelecionarPorIdAsync(request.Id);

        if (grupoAutomovelSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        var resposta = new SelecionarGrupoAutomovelPorIdResponse(
            grupoAutomovelSelecionado.Id,
            grupoAutomovelSelecionado.Nome
        );

        return Result.Ok(resposta);
    }
}