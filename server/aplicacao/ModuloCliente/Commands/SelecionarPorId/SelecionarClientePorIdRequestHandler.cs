using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarPorId;

public class SelecionarClientePorIdRequestHandler(
    IRepositorioCliente repositorioCliente
) : IRequestHandler<SelecionarClientePorIdRequest, Result<SelecionarClientePorIdResponse>>
{
    public async Task<Result<SelecionarClientePorIdResponse>> Handle(SelecionarClientePorIdRequest request, CancellationToken cancellationToken)
    {
        var clienteSelecionado = await repositorioCliente.SelecionarPorIdAsync(request.Id);

        if (clienteSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        var resposta = new SelecionarClientePorIdResponse(
            clienteSelecionado.Id,
            clienteSelecionado.Nome,
            clienteSelecionado.Endereco,
            clienteSelecionado.Telefone,
            clienteSelecionado.ClienteTipo,
            clienteSelecionado.Documento,
            clienteSelecionado.Cnh,
            clienteSelecionado.Condutor
        );

        return Result.Ok(resposta);
    }
}