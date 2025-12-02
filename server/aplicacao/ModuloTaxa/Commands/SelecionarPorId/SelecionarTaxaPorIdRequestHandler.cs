using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarPorId;

public class SelecionarTaxaPorIdRequestHandler(
    IRepositorioTaxa repositorioTaxa
) : IRequestHandler<SelecionarTaxaPorIdRequest, Result<SelecionarTaxaPorIdResponse>>
{
    public async Task<Result<SelecionarTaxaPorIdResponse>> Handle(SelecionarTaxaPorIdRequest request, CancellationToken cancellationToken)
    {
        var taxaSelecionado = await repositorioTaxa.SelecionarPorIdAsync(request.Id);

        if (taxaSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        var resposta = new SelecionarTaxaPorIdResponse(
            taxaSelecionado.Id,
            taxaSelecionado.Nome,
            taxaSelecionado.Preco,
            taxaSelecionado.Servico
        );

        return Result.Ok(resposta);
    }
}