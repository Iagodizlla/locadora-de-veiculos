using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarPorId;

public class SelecionarAluguelPorIdRequestHandler(
    IRepositorioAluguel repositorioAluguel
) : IRequestHandler<SelecionarAluguelPorIdRequest, Result<SelecionarAluguelPorIdResponse>>
{
    public async Task<Result<SelecionarAluguelPorIdResponse>> Handle(SelecionarAluguelPorIdRequest request, CancellationToken cancellationToken)
    {
        var aluguelSelecionado = await repositorioAluguel.SelecionarPorIdAsync(request.Id);

        if (aluguelSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        var resposta = new SelecionarAluguelPorIdResponse(
            aluguelSelecionado.Id,
            aluguelSelecionado.Cliente, 
            aluguelSelecionado.Condutor,
            aluguelSelecionado.Automovel,
            aluguelSelecionado.Plano,
            aluguelSelecionado.Taxas,
            aluguelSelecionado.DataSaida,
            aluguelSelecionado.DataRetornoPrevista,
            aluguelSelecionado.DataDevolucao,
            aluguelSelecionado.QuilometragemInicial,
            aluguelSelecionado.QuilometragemFinal,
            aluguelSelecionado.NivelCombustivelNaSaida,
            aluguelSelecionado.NivelCombustivelNaDevolucao,
            aluguelSelecionado.SeguroCliente,
            aluguelSelecionado.SeguroTerceiro,
            aluguelSelecionado.ValorSeguroPorDia,
            aluguelSelecionado.Status
        );

        return Result.Ok(resposta);
    }
}