using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Inserir;

public class InserirGrupoAutomovelRequestHandler(
    IContextoPersistencia contexto,
    IRepositorioGrupoAutomovel repositorioGrupoAutomovel,
    ITenantProvider tenantProvider,
    IValidator<GrupoAutomovel> validador
) : IRequestHandler<InserirGrupoAutomovelRequest, Result<InserirGrupoAutomovelResponse>>
{
    public async Task<Result<InserirGrupoAutomovelResponse>> Handle(
        InserirGrupoAutomovelRequest request, CancellationToken cancellationToken)
    {
        var grupoAutomovel = new GrupoAutomovel(request.Nome)
        {
            EmpresaId = tenantProvider.EmpresaId.GetValueOrDefault()
        };

        // validações
        var resultadoValidacao = await validador.ValidateAsync(grupoAutomovel);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
               .Select(failure => failure.ErrorMessage)
               .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var grupoAutomoveisRegistrados = await repositorioGrupoAutomovel.SelecionarTodosAsync();

        if (NomeDuplicado(grupoAutomovel, grupoAutomoveisRegistrados))
            return Result.Fail(GrupoAutomovelErrorResults.NomeDuplicadoError(grupoAutomovel.Nome));

        // inserção
        try
        {
            await repositorioGrupoAutomovel.InserirAsync(grupoAutomovel);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new InserirGrupoAutomovelResponse(grupoAutomovel.Id));
    }

    private bool NomeDuplicado(GrupoAutomovel grupoAutomovel, IList<GrupoAutomovel> grupoAutomoveis)
    {
        return grupoAutomoveis
            .Any(registro => string.Equals(
                registro.Nome,
                grupoAutomovel.Nome,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}
