using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarFinalizados;

public class SelecionarAlugueisFinalizadosRequestHandler(
    IRepositorioAluguel repositorioAluguel
) : IRequestHandler<SelecionarAlugueisFinalizadosRequest, Result<SelecionarAlugueisFinalizadosResponse>>
{
    public async Task<Result<SelecionarAlugueisFinalizadosResponse>> Handle(SelecionarAlugueisFinalizadosRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioAluguel.SelecionarFinalizadosAsync();

        var response = new SelecionarAlugueisFinalizadosResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarAlugueisFinalizadosDto(r.Id, r.Cliente, r.Condutor, r.Automovel, r.Plano, r.Taxas, r.DataSaida, r.DataRetornoPrevista,
                r.DataDevolucao, r.QuilometragemInicial, r.QuilometragemFinal, r.NivelCombustivelNaSaida, r.NivelCombustivelNaDevolucao, r.SeguroCliente,
                r.SeguroTerceiro, r.ValorSeguroPorDia, r.Status, r.ValorTotal))
        };

        return Result.Ok(response);
    }
}