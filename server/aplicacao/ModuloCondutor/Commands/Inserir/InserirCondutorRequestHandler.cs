using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Inserir;

public class InserirCondutorRequestHandler(
    IRepositorioCliente repositorioCliente,
    IContextoPersistencia contexto,
    IRepositorioCondutor repositorioCondutor,
    ITenantProvider tenantProvider,
    IValidator<Condutor> validador
) : IRequestHandler<InserirCondutorRequest, Result<InserirCondutorResponse>>
{
    public async Task<Result<InserirCondutorResponse>> Handle(
        InserirCondutorRequest request, CancellationToken cancellationToken)

    {
        var cliente = await repositorioCliente.SelecionarPorIdAsync(request.ClienteId);

        if (request.ECliente == false)
        {
            if (ClienteNaoEncontrado(cliente))
                return Result.Fail(CondutorErrorResults.ClienteNaoEncontradoError(request.ClienteId));
        }

        var condutor = new Condutor(request.Nome, request.Cnh, request.Cpf, request.Telefone, request.Categoria, request.ValidadeCnh, cliente, request.ECliente)
        {
            EmpresaId = tenantProvider.EmpresaId.GetValueOrDefault()
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

        if (CpfDuplicado(condutor, condutoresRegistrados))
            return Result.Fail(CondutorErrorResults.CpfDuplicadoError(condutor.Cpf));

        if (TelefoneDuplicado(condutor, condutoresRegistrados))
            return Result.Fail(CondutorErrorResults.TelefoneDuplicadoError(condutor.Telefone));

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
    public bool CpfDuplicado(Condutor condutor, IList<Condutor> condutores)
    {
        return condutores
            .Any(registro => string.Equals(
                registro.Cnh,
                condutor.Cnh,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
    public bool TelefoneDuplicado(Condutor condutor, IList<Condutor> condutores)
    {
        return condutores
            .Any(registro => string.Equals(
                registro.Telefone,
                condutor.Telefone,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }

    public bool ClienteNaoEncontrado(Cliente cliente)
    {
        return cliente == null;
    }
}
