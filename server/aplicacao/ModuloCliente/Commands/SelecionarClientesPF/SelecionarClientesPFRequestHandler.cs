using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarClientesPF;

public class SelecionarClientesPFRequestHandler(
    IRepositorioCliente repositorioCliente
) : IRequestHandler<SelecionarClientesPFRequest, Result<SelecionarClientesPFResponse>>
{
    public async Task<Result<SelecionarClientesPFResponse>> Handle(
        SelecionarClientesPFRequest request,
        CancellationToken cancellationToken)
    {
        var registros = await repositorioCliente.SelecionarClientesPFAsync();

        var response = new SelecionarClientesPFResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(c => new SelecionarClientesPFDto(
                    c.Id,
                    c.Nome,
                    c.Endereco,
                    c.Telefone,
                    c.Documento,
                    c.Cnh
                ))
        };

        return Result.Ok(response);
    }
}
