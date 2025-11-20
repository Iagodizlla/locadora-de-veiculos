using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.Inserir;

public class InserirAutomovelRequestHandler(
    IContextoPersistencia contexto,
    IRepositorioAutomovel repositorioAutomovel,
    IRepositorioGrupoAutomovel repositorioGrupo,
    ITenantProvider tenantProvider,
    IValidator<Automovel> validador
) : IRequestHandler<InserirAutomovelRequest, Result<InserirAutomovelResponse>>
{
    public async Task<Result<InserirAutomovelResponse>> Handle(
        InserirAutomovelRequest request, CancellationToken cancellationToken)
    {
        var grupo = await repositorioGrupo.SelecionarPorIdAsync(request.GrupoAutomovelId);

        if (GrupoNaoEncontrado(grupo))
            return Result.Fail(AutomovelErrorResults.GrupoNaoEncontradoError());

        var automovel = new Automovel(request.Placa, request.Marca, request.Modelo, request.Cor, request.Ano, request.CapacidadeTanque, grupo, request.Foto, request.Combustivel)
        {
            EmpresaId = tenantProvider.EmpresaId.GetValueOrDefault()
        };

        // validações
        var resultadoValidacao = await validador.ValidateAsync(automovel);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
               .Select(failure => failure.ErrorMessage)
               .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var automoveisRegistrados = await repositorioAutomovel.SelecionarTodosAsync();

        if (PlacaDuplicado(automovel, automoveisRegistrados))
            return Result.Fail(AutomovelErrorResults.PlacaDuplicadoError(automovel.Placa));

        // inserção
        try
        {
            await repositorioAutomovel.InserirAsync(automovel);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new InserirAutomovelResponse(automovel.Id));
    }

    private bool PlacaDuplicado(Automovel automovel, IList<Automovel> automoveis)
    {
        return automoveis
            .Any(registro => string.Equals(
                registro.Placa,
                automovel.Placa,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
    private bool GrupoNaoEncontrado(GrupoAutomovel grupo)
    {
        return grupo == null;
    }
}
