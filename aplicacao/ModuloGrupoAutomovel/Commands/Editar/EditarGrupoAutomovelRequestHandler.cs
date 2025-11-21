using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Editar;

public class EditarGrupoAutomovelRequestHandler(
    IRepositorioGrupoAutomovel repositorioGrupoAutomovel,
    IContextoPersistencia contexto,
    IValidator<GrupoAutomovel> validador
) : IRequestHandler<EditarGrupoAutomovelRequest, Result<EditarGrupoAutomovelResponse>>
{
    public async Task<Result<EditarGrupoAutomovelResponse>> Handle(EditarGrupoAutomovelRequest request, CancellationToken cancellationToken)
    {
        var grupoAutomovelSelecionado = await repositorioGrupoAutomovel.SelecionarPorIdAsync(request.Id);

        if (grupoAutomovelSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        grupoAutomovelSelecionado.Nome = request.Nome;

        var resultadoValidacao = 
            await validador.ValidateAsync(grupoAutomovelSelecionado, cancellationToken);
        
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var grupoAutomoveis = await repositorioGrupoAutomovel.SelecionarTodosAsync();

        if (NomeDuplicado(grupoAutomovelSelecionado, grupoAutomoveis))
            return Result.Fail(GrupoAutomovelErrorResults.NomeDuplicadoError(grupoAutomovelSelecionado.Nome));
        
        try
        {
            await repositorioGrupoAutomovel.EditarAsync(grupoAutomovelSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }
 
        return Result.Ok(new EditarGrupoAutomovelResponse(grupoAutomovelSelecionado.Id));
    }
    
    private bool NomeDuplicado(GrupoAutomovel grupoAutomovel, IList<GrupoAutomovel> grupoAutomoveis)
    {
        return grupoAutomoveis
            .Where(r => r.Id != grupoAutomovel.Id)
            .Any(registro => string.Equals(
                registro.Nome,
                grupoAutomovel.Nome,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}