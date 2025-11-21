using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Excluir;

public class ExcluirClienteRequestHandler(
    IRepositorioCliente repositorioCliente,
    IContextoPersistencia contexto
) : IRequestHandler<ExcluirClienteRequest, Result<ExcluirClienteResponse>>
{
    public async Task<Result<ExcluirClienteResponse>> Handle(ExcluirClienteRequest request, CancellationToken cancellationToken)
    {
        var clienteSelecionado = await repositorioCliente.SelecionarPorIdAsync(request.Id);

        if (clienteSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        try
        {
            await repositorioCliente.ExcluirAsync(clienteSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirClienteResponse());
    }
}