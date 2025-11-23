using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Editar;

public class EditarCondutorRequestHandler(
    IRepositorioCondutor repositorioCondutor,
    IContextoPersistencia contexto,
    IValidator<Condutor> validador
) : IRequestHandler<EditarCondutorRequest, Result<EditarCondutorResponse>>
{
    public async Task<Result<EditarCondutorResponse>> Handle(EditarCondutorRequest request, CancellationToken cancellationToken)
    {
        var condutorSelecionado = await repositorioCondutor.SelecionarPorIdAsync(request.Id);

        if (condutorSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        condutorSelecionado.Nome = request.Nome;
        condutorSelecionado.Cnh = request.Cnh;
        condutorSelecionado.Telefone = request.Telefone;
        condutorSelecionado.Categoria = request.Categoria;
        condutorSelecionado.ValidadeCnh = request.ValidadeCnh;
        condutorSelecionado.Cliente = request.Cliente;
        condutorSelecionado.ECliente = request.ECliente;

        var resultadoValidacao = 
            await validador.ValidateAsync(condutorSelecionado, cancellationToken);
        
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var condutores = await repositorioCondutor.SelecionarTodosAsync();

        if (CnhDuplicado(condutorSelecionado, condutores))
            return Result.Fail(CondutorErrorResults.CnhDuplicadoError(condutorSelecionado.Cnh));

        if (CpfDuplicado(condutorSelecionado, condutores))
            return Result.Fail(CondutorErrorResults.CpfDuplicadoError(condutorSelecionado.Cpf));

        if (TelefoneDuplicado(condutorSelecionado, condutores))
            return Result.Fail(CondutorErrorResults.TelefoneDuplicadoError(condutorSelecionado.Telefone));

        if (condutorSelecionado.ECliente == true)
        {
            if (ClienteNaoEncontrado(condutorSelecionado.Cliente))
                return Result.Fail(CondutorErrorResults.ClienteNaoEncontradoError(condutorSelecionado.Cliente.Id));
        }

        try
        {
            await repositorioCondutor.EditarAsync(condutorSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }
 
        return Result.Ok(new EditarCondutorResponse(condutorSelecionado.Id));
    }
    
    private bool CnhDuplicado(Condutor condutor, IList<Condutor> condutores)
    {
        return condutores
            .Where(r => r.Id != condutor.Id)
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