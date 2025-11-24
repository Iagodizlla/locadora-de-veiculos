using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarTodos;

public class SelecionarAlugueisRequestHandler(
    IRepositorioAluguel repositorioAluguel
) : IRequestHandler<SelecionarAlugueisRequest, Result<SelecionarAlugueisResponse>>
{
    public async Task<Result<SelecionarAlugueisResponse>> Handle(SelecionarAlugueisRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioAluguel.SelecionarTodosAsync();

        var response = new SelecionarAlugueisResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarAlugueisDto(r.Id, r.Cliente, r.Condutor, r.Automovel, r.Plano, r.Taxas, r.DataSaida, r.DataRetornoPrevista,
                r.DataDevolucao, r.QuilometragemInicial, r.QuilometragemFinal, r.NivelCombustivelNaSaida, r.NivelCombustivelNaDevolucao, r.SeguroCliente,
                r.SeguroTerceiro, r.ValorSeguroPorDia, r.Status))
        };

        return Result.Ok(response);
    }
}