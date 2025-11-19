using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Inserir;

public class InserirCondutorRequestHandler(
    IContextoPersistencia contexto,
    IRepositorioCondutor repositorioCondutor,
    ITenantProvider tenantProvider,
    IValidator<Condutor> validador
) : IRequestHandler<InserirCondutorRequest, Result<InserirCondutorResponse>>
{
    public async Task<Result<InserirCondutorResponse>> Handle(
        InserirCondutorRequest request, CancellationToken cancellationToken)
    {
        var condutor = new Condutor(request.Nome, request.Cnh, request.Categoria, request.ValidadeCnh)
        {
            UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault()
        };

        // validações
        var resultadoValidacao = await validador.ValidateAsync(condutor);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
               .Select(failure => failure.ErrorMessage)
               .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var condutoresRegistrados = await repositorioCondutor.SelecionarTodosAsync();

        if (CnhDuplicado(condutor, condutoresRegistrados))
            return Result.Fail(CondutorErrorResults.CnhDuplicadoError(condutor.Cnh));

        // inserção
        try
        {
            await repositorioCondutor.InserirAsync(condutor);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new InserirCondutorResponse(condutor.Id));
    }

    private bool CnhDuplicado(Condutor condutor, IList<Condutor> condutores)
    {
        return condutores
            .Any(registro => string.Equals(
                registro.Cnh,
                condutor.Cnh,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}
