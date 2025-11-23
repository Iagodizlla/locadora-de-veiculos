using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarPorGrupo;

public class SelecionarAutomoveisPorGrupoRequestHandler(
    IRepositorioAutomovel repositorioAutomovel
) : IRequestHandler<SelecionarAutomoveisPorGrupoRequest, Result<SelecionarAutomoveisPorGrupoResponse>>
{
    public async Task<Result<SelecionarAutomoveisPorGrupoResponse>> Handle(
        SelecionarAutomoveisPorGrupoRequest request, CancellationToken cancellationToken)
    {
        var automoveis = await repositorioAutomovel.SelecionarPorGrupoAsync(request.GrupoId);

        if (automoveis is null)
            return Result.Fail(ErrorResults.NotFoundError(request.GrupoId));

        var response = new SelecionarAutomoveisPorGrupoResponse
        {
            QuantidadeRegistros = automoveis.Count,
            Registros = automoveis.Select(a =>
                new SelecionarAutomoveisPorGrupoDto(
                    a.Id,
                    a.Placa,
                    a.Modelo,
                    a.Marca,
                    a.Cor,
                    a.Ano,
                    a.CapacidadeTanque,
                    a.Foto,
                    a.Combustivel
                )
            )
        };

        return Result.Ok(response);
    }
}
