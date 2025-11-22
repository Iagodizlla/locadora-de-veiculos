using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.SelecionarTodos;

public class SelecionarPlanosRequestHandler(
    IRepositorioPlano repositorioPlano
) : IRequestHandler<SelecionarPlanosRequest, Result<SelecionarPlanosResponse>>
{
    public async Task<Result<SelecionarPlanosResponse>> Handle(SelecionarPlanosRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioPlano.SelecionarTodosAsync();

        var response = new SelecionarPlanosResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarPlanosDto(r.Id, r.GrupoAutomovel, r.PrecoDiario, r.PrecoDiarioControlado, r.PrecoPorKm, r.KmLivres, r.PrecoPorKmExplorado, r.PrecoLivre))
        };

        return Result.Ok(response);
    }
}