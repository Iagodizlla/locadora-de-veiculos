using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Editar;

public class EditarTaxaRequestHandler(
    IRepositorioTaxa repositorioTaxa,
    IContextoPersistencia contexto,
    IValidator<Taxa> validador
) : IRequestHandler<EditarTaxaRequest, Result<EditarTaxaResponse>>
{
    public async Task<Result<EditarTaxaResponse>> Handle(EditarTaxaRequest request, CancellationToken cancellationToken)
    {
        var taxaSelecionado = await repositorioTaxa.SelecionarPorIdAsync(request.Id);

        if (taxaSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        taxaSelecionado.Nome = request.Nome;
        taxaSelecionado.Preco = request.Preco;
        taxaSelecionado.Servico = request.Servico;
        taxaSelecionado.Alugueis = request.Alugueis;

        var resultadoValidacao = 
            await validador.ValidateAsync(taxaSelecionado, cancellationToken);
        
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var taxas = await repositorioTaxa.SelecionarTodosAsync();

        if (NomeDuplicado(taxaSelecionado, taxas))
            return Result.Fail(TaxaErrorResults.NomeDuplicadoError(taxaSelecionado.Nome));
        
        try
        {
            await repositorioTaxa.EditarAsync(taxaSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }
 
        return Result.Ok(new EditarTaxaResponse(taxaSelecionado.Id));
    }
    
    private bool NomeDuplicado(Taxa taxa, IList<Taxa> taxas)
    {
        return taxas
            .Where(r => r.Id != taxa.Id)
            .Any(registro => string.Equals(
                registro.Nome,
                taxa.Nome,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}