using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarTodos;

public class SelecionarTaxasRequestHandler(
    IRepositorioTaxa repositorioTaxa
) : IRequestHandler<SelecionarTaxasRequest, Result<SelecionarTaxasResponse>>
{
    public async Task<Result<SelecionarTaxasResponse>> Handle(SelecionarTaxasRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioTaxa.SelecionarTodosAsync();

        var response = new SelecionarTaxasResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarTaxasDto(r.Id, r.Nome, r.Preco, r.Servico, r.Alugueis))
        };

        return Result.Ok(response);
    }
}