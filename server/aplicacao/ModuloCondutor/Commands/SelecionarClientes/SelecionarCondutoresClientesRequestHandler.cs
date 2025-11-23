using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarClientes;

public class SelecionarCondutoresClientesRequestHandler(
    IRepositorioCondutor repositorioCondutor
) : IRequestHandler<SelecionarCondutoresClientesRequest, Result<SelecionarCondutoresClientesResponse>>
{
    public async Task<Result<SelecionarCondutoresClientesResponse>> Handle(SelecionarCondutoresClientesRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioCondutor.SelecionarClientesAsync();

        var response = new SelecionarCondutoresClientesResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarCondutoresClientesDto(
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
