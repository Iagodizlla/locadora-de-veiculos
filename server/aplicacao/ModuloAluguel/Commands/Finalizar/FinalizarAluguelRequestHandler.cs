using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Finalizar;

public class FinalizarAluguelRequestHandler(
    IRepositorioAluguel repositorioAluguel,
    IContextoPersistencia contexto,
    IValidator<Aluguel> validador
) : IRequestHandler<FinalizarAluguelRequest, Result<FinalizarAluguelResponse>>
{
    public async Task<Result<FinalizarAluguelResponse>> Handle(FinalizarAluguelRequest request, CancellationToken cancellationToken)
    {
        var aluguel = await repositorioAluguel.SelecionarPorIdAsync(request.Id);

        if (aluguel is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        aluguel.DataDevolucao = request.DataDevolucao;
        aluguel.QuilometragemFinal = request.KmFinal;
        aluguel.NivelCombustivelNaDevolucao = request.NivelCombustivelNaDevolucao;
        aluguel.Status = false;

        var resultadoValidacao = 
            await validador.ValidateAsync(aluguel, cancellationToken);
        
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        try
        {
            await repositorioAluguel.EditarAsync(aluguel);

            await repositorioAluguel.CalcularValorTotalDoAluguelAsync(aluguel);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new FinalizarAluguelResponse(aluguel.Id));
    }
}
