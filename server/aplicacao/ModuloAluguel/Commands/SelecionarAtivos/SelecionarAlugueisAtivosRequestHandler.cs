using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarAtivos;

public class SelecionarAlugueisAtivosRequestHandler(
    IRepositorioAluguel repositorioAluguel
) : IRequestHandler<SelecionarAlugueisAtivosRequest, Result<SelecionarAlugueisAtivosResponse>>
{
    public async Task<Result<SelecionarAlugueisAtivosResponse>> Handle(SelecionarAlugueisAtivosRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioAluguel.SelecionarAtivosAsync();

        var response = new SelecionarAlugueisAtivosResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarAlugueisAtivosDto(r.Id, r.Cliente, r.Condutor, r.Automovel, r.Plano, r.Taxas, r.DataSaida, r.DataRetornoPrevista,
                r.DataDevolucao, r.QuilometragemInicial, r.QuilometragemFinal, r.NivelCombustivelNaSaida, r.NivelCombustivelNaDevolucao, r.SeguroCliente,
                r.SeguroTerceiro, r.ValorSeguroPorDia, r.Status, r.ValorTotal))
        };

        return Result.Ok(response);
    }
}