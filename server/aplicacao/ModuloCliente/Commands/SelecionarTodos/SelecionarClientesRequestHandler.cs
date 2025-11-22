using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarTodos;

public class SelecionarClientesRequestHandler(
    IRepositorioCliente repositorioCliente
) : IRequestHandler<SelecionarClientesRequest, Result<SelecionarClientesResponse>>
{
    public async Task<Result<SelecionarClientesResponse>> Handle(SelecionarClientesRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioCliente.SelecionarTodosAsync();

        var response = new SelecionarClientesResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarClientesDto(r.Id, r.Nome, r.Endereco, r.Telefone, r.ClienteTipo, r.Documento, r.Cnh, r.Condutor))
        };

        return Result.Ok(response);
    }
}