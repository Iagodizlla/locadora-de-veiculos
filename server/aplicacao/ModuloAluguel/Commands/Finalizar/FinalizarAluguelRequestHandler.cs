using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Finalizar;

public class FinalizarAluguelRequestHandler(
    IRepositorioAluguel repositorioAluguel,
    IContextoPersistencia contexto
) : IRequestHandler<FinalizarAluguelRequest, Result<FinalizarAluguelResponse>>
{
    public async Task<Result<FinalizarAluguelResponse>> Handle(FinalizarAluguelRequest request, CancellationToken cancellationToken)
    {
        var aluguel = await repositorioAluguel.SelecionarPorIdAsync(request.Id);

        if (aluguel is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        // marca como finalizado
        aluguel.Status = true;

        // chama o método FinalizarAsync do repositório
        var atualizado = await repositorioAluguel.FinalizarAsync(aluguel);

        if (!atualizado)
            return Result.Fail("Não foi possível finalizar o aluguel.");

        await contexto.GravarAsync();

        var response = new FinalizarAluguelResponse(aluguel.Id, aluguel.Status);

        return Result.Ok(response);
    }
}
