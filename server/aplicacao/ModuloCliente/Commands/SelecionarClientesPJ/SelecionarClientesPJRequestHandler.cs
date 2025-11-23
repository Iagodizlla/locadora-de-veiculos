using FluentResults;
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
        var todos = await repositorioCliente.SelecionarTodosAsync();

        var pj = todos
            .Where(c => c.ClienteTipo == ETipoCliente.PessoaJuridica)
            .Select(c => new SelecionarClientesPJDto(
                c.Id,
                c.Nome,
                c.Endereco,
                c.Telefone,
                c.Documento,
                c.Cnh,
                c.Condutor
            ))
            .ToList();

        var response = new SelecionarClientesPJResponse
        {
            QuantidadeRegistros = pj.Count,
            Registros = pj
        };

        return Result.Ok(response);
    }
}
