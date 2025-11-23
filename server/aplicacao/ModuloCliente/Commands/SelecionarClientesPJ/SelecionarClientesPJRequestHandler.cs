using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarClientesPF;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarClientesPJ;

public class SelecionarClientesPJRequestHandler(
    IRepositorioCliente repositorioCliente
) : IRequestHandler<SelecionarClientesPJRequest, Result<SelecionarClientesPJResponse>>
{
    public async Task<Result<SelecionarClientesPJResponse>> Handle(
        SelecionarClientesPJRequest request,
        CancellationToken cancellationToken)
    {
        var registros = await repositorioCliente.SelecionarClientesPJAsync();

        var response = new SelecionarClientesPJResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(c => new SelecionarClientesPJDto(
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
