using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.CalcularValor;

public class CalcularValorAluguelRequestHandler(
IRepositorioAluguel repositorioAluguel
) : IRequestHandler<CalcularValorAluguelRequest, Result<CalcularValorAluguelResponse>>
{
    public async Task<Result<CalcularValorAluguelResponse>> Handle(CalcularValorAluguelRequest request, CancellationToken cancellationToken)
    {// 1. Busca o Aluguel pelo ID
        // Como o infra já salvou os dados de devolução, este objeto 'aluguel' já deve vir completo.
        var aluguel = await repositorioAluguel.SelecionarPorIdAsync(request.Id);

        if (aluguel is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        if (aluguel.DataDevolucao is null || aluguel.QuilometragemFinal is null || aluguel.NivelCombustivelNaDevolucao is null)
        {
            return Result.Fail("Dados de devolução incompletos no banco de dados para realizar o cálculo final.");
        }

        decimal valorTotal = await repositorioAluguel.CalcularValorTotalDoAluguelAsync(aluguel);

        var response = new CalcularValorAluguelResponse(valorTotal);

        return Result.Ok(response);
    }
}
