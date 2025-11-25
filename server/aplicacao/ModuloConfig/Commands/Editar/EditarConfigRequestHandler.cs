using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloConfig;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloConfig.Commands.Editar;

public class EditarConfigRequestHandler(
    IRepositorioConfig repositorioConfig,
    IValidator<Config> validador,
    IContextoPersistencia contexto
) : IRequestHandler<EditarConfigRequest, Result<EditarConfigResponse>>
{
    public async Task<Result<EditarConfigResponse>> Handle(
        EditarConfigRequest request,
        CancellationToken cancellationToken)
    {
        var configuracao = await repositorioConfig.SelecionarAsync();

        if (configuracao == null)
            return Result.Fail(ErrorResults.NotFoundError(configuracao.Id));

        configuracao.Gasolina = request.Gasolina;
        configuracao.Gas = request.Gas;
        configuracao.Diessel = request.Diesel;
        configuracao.Alcool = request.Alcool;

        var resultadoValidacao = await validador.ValidateAsync(configuracao);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.Select(x => x.ErrorMessage).ToList();
            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        try
        {
            await repositorioConfig.EditarAsync(configuracao);
            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new EditarConfigResponse(configuracao.Id));
    }
}