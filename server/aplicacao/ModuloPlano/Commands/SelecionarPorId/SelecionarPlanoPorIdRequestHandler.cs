using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.SelecionarPorId;

public class SelecionarPlanoPorIdRequestHandler(
    IRepositorioPlano repositorioPlano
) : IRequestHandler<SelecionarPlanoPorIdRequest, Result<SelecionarPlanoPorIdResponse>>
{
    public async Task<Result<SelecionarPlanoPorIdResponse>> Handle(SelecionarPlanoPorIdRequest request, CancellationToken cancellationToken)
    {
        var planoSelecionado = await repositorioPlano.SelecionarPorIdAsync(request.Id);

        if (planoSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        var resposta = new SelecionarPlanoPorIdResponse(
            planoSelecionado.Id,
            planoSelecionado.GrupoAutomovel,
            planoSelecionado.PrecoDiario,
            planoSelecionado.PrecoDiarioControlado,
            planoSelecionado.PrecoPorKm,
            planoSelecionado.KmLivres,
            planoSelecionado.PrecoPorKmExplorado,
            planoSelecionado.PrecoLivre
        );

        return Result.Ok(resposta);
    }
}