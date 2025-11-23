using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Inserir;

public class InserirTaxaRequestHandler(
    IContextoPersistencia contexto,
    IRepositorioTaxa repositorioTaxa,
    ITenantProvider tenantProvider,
    IValidator<Taxa> validador
) : IRequestHandler<InserirTaxaRequest, Result<InserirTaxaResponse>>
{
    public async Task<Result<InserirTaxaResponse>> Handle(
        InserirTaxaRequest request, CancellationToken cancellationToken)
    {
        var taxa = new Taxa(request.Nome, request.Preco, request.Servico)
        {
            EmpresaId = tenantProvider.EmpresaId.GetValueOrDefault()
        };

        // validações
        var resultadoValidacao = await validador.ValidateAsync(taxa);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
               .Select(failure => failure.ErrorMessage)
               .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var taxasRegistrados = await repositorioTaxa.SelecionarTodosAsync();

        if (NomeDuplicado(taxa, taxasRegistrados))
            return Result.Fail(TaxaErrorResults.NomeDuplicadoError(taxa.Nome));

        // inserção
        try
        {
            await repositorioTaxa.InserirAsync(taxa);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new InserirTaxaResponse(taxa.Id));
    }

    private bool NomeDuplicado(Taxa taxa, IList<Taxa> taxas)
    {
        return taxas
            .Any(registro => string.Equals(
                registro.Nome,
                taxa.Nome,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}
