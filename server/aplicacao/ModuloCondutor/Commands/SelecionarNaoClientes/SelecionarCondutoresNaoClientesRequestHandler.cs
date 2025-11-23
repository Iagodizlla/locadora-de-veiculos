using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarNaoClientes;

public class SelecionarCondutoresNaoClientesRequestHandler(
    IRepositorioCondutor repositorioCondutor
) : IRequestHandler<SelecionarCondutoresNaoClientesRequest, Result<SelecionarCondutoresNaoClientesResponse>>
{
    public async Task<Result<SelecionarCondutoresNaoClientesResponse>> Handle(SelecionarCondutoresNaoClientesRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioCondutor.SelecionarNaoClientesAsync();

        var response = new SelecionarCondutoresNaoClientesResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarCondutoresNaoClientesDto(
                    r.Id,
                    r.Nome,
                    r.Cnh,
                    r.Cpf,
                    r.Telefone,
                    r.Categoria,
                    r.ValidadeCnh,
                    r.ECliente
                ))
        };

        return Result.Ok(response);
    }
}
