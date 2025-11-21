using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Inserir;

public class InserirPlanoRequestHandler(
    IContextoPersistencia contexto,
    IRepositorioPlano repositorioPlano,
    IRepositorioGrupoAutomovel repositorioGrupo,
    ITenantProvider tenantProvider,
    IValidator<Plano> validador
) : IRequestHandler<InserirPlanoRequest, Result<InserirPlanoResponse>>
{
    public async Task<Result<InserirPlanoResponse>> Handle(
        InserirPlanoRequest request, CancellationToken cancellationToken)
    {
        var grupo = await repositorioGrupo.SelecionarPorIdAsync(request.GrupoAutomovelId);

        if (GrupoNaoEncontrado(grupo))
            return Result.Fail(PlanoErrorResults.GrupoNaoEncontradoError());

        var plano = new Plano(request.TipoPlano, grupo, request.PrecoDiario, request.PrecoPorKm, request.KmLivres, request.PrecoporKmExplorado, request.PrecoLivre)
        {
            EmpresaId = tenantProvider.EmpresaId.GetValueOrDefault()
        };

        // validações
        var resultadoValidacao = await validador.ValidateAsync(plano);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
               .Select(failure => failure.ErrorMessage)
               .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var automoveisRegistrados = await repositorioPlano.SelecionarTodosAsync();

        if (GrupoDuplicado(plano, automoveisRegistrados))
            return Result.Fail(PlanoErrorResults.GrupoDuplicadoError(plano.GrupoAutomovel.Nome));

        // inserção
        try
        {
            await repositorioPlano.InserirAsync(plano);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new InserirPlanoResponse(plano.Id));
    }

    private bool GrupoNaoEncontrado(GrupoAutomovel grupo)
    {
        return grupo == null;
    }

    private bool GrupoDuplicado(Plano plano, IList<Plano> planos)
    {
        return planos.Any(p => p.GrupoAutomovel.Id == plano.GrupoAutomovel.Id &&
                               p.Id != plano.Id);
    }
}
