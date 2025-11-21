using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Editar;

public class EditarPlanoRequestHandler(
    IRepositorioPlano repositorioPlano,
    IContextoPersistencia contexto,
    IValidator<Plano> validador
) : IRequestHandler<EditarPlanoRequest, Result<EditarPlanoResponse>>
{
    public async Task<Result<EditarPlanoResponse>> Handle(EditarPlanoRequest request, CancellationToken cancellationToken)
    {
        var planoSelecionado = await repositorioPlano.SelecionarPorIdAsync(request.Id);

        if (planoSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        planoSelecionado.TipoPlano = request.TipoPlano;
        planoSelecionado.GrupoAutomovel = request.GrupoAutomovel;
        planoSelecionado.PrecoDiario = request.PrecoDiario;
        planoSelecionado.PrecoPorKm = request.PrecoPorKm;
        planoSelecionado.KmLivres = request.KmLivres;
        planoSelecionado.PrecoPorKmExplorado = request.PrecoporKmExplorado;
        planoSelecionado.PrecoLivre = request.PrecoLivre;

        var resultadoValidacao = 
            await validador.ValidateAsync(planoSelecionado, cancellationToken);
        
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var planos = await repositorioPlano.SelecionarTodosAsync();

        if(GrupoNaoEncontrado(planoSelecionado))
            return Result.Fail(PlanoErrorResults.GrupoNaoEncontradoError());

        if(GrupoDuplicado(planoSelecionado, planos))
            return Result.Fail(PlanoErrorResults.GrupoDuplicadoError(planoSelecionado.GrupoAutomovel.Nome));

        try
        {
            await repositorioPlano.EditarAsync(planoSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }
 
        return Result.Ok(new EditarPlanoResponse(planoSelecionado.Id));
    }

    private bool GrupoNaoEncontrado(Plano plano)
    {
        return plano.GrupoAutomovel == null;
    }

    private bool GrupoDuplicado(Plano plano, IList<Plano> planos)
    {
        return planos.Any(p => p.GrupoAutomovel.Id == plano.GrupoAutomovel.Id &&
                               p.Id != plano.Id);
    }
}