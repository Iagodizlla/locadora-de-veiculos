using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Excluir;

public class ExcluirCondutorRequestHandler(
    IRepositorioCondutor repositorioCondutor,
    IContextoPersistencia contexto
) : IRequestHandler<ExcluirCondutorRequest, Result<ExcluirCondutorResponse>>
{
    public async Task<Result<ExcluirCondutorResponse>> Handle(ExcluirCondutorRequest request, CancellationToken cancellationToken)
    {
        var condutorSelecionado = await repositorioCondutor.SelecionarPorIdAsync(request.Id);

        if (condutorSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        try
        {
            await repositorioCondutor.ExcluirAsync(condutorSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirCondutorResponse());
    }
}