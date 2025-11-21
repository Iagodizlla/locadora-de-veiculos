using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarTodos;

public class SelecionarAutomoveisRequestHandler(
    IRepositorioAutomovel repositorioAutomovel
) : IRequestHandler<SelecionarAutomoveisRequest, Result<SelecionarAutomoveisResponse>>
{
    public async Task<Result<SelecionarAutomoveisResponse>> Handle(SelecionarAutomoveisRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioAutomovel.SelecionarTodosAsync();

        var response = new SelecionarAutomoveisResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarAutomoveisDto(r.Id, r.Placa, r.Modelo, r.Marca, r.Cor, r.Ano, r.CapacidadeTanque, r.Foto, r.GrupoAutomovel, r.Combustivel))
        };

        return Result.Ok(response);
    }
}