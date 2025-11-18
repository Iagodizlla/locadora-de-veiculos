using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.SelecionarTodos;

public class SelecionarGrupoAutomoveisRequestHandler(
    IRepositorioGrupoAutomovel repositorioGrupoAutomovel
) : IRequestHandler<SelecionarGrupoAutomoveisRequest, Result<SelecionarGrupoAutomoveisResponse>>
{
    public async Task<Result<SelecionarGrupoAutomoveisResponse>> Handle(SelecionarGrupoAutomoveisRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioGrupoAutomovel.SelecionarTodosAsync();

        var response = new SelecionarGrupoAutomoveisResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarGrupoAutomoveisDto(r.Id, r.Nome))
        };

        return Result.Ok(response);
    }
}