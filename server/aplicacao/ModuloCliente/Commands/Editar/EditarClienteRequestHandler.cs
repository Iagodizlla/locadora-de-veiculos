using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Editar;

public class EditarClienteRequestHandler(
    IRepositorioCliente repositorioCliente,
    IContextoPersistencia contexto,
    IValidator<Cliente> validador
) : IRequestHandler<EditarClienteRequest, Result<EditarClienteResponse>>
{
    public async Task<Result<EditarClienteResponse>> Handle(EditarClienteRequest request, CancellationToken cancellationToken)
    {
        var endereco = new Endereco(
           request.Endereco.Logradouro,
           request.Endereco.Numero,
           request.Endereco.Bairro,
           request.Endereco.Cidade,
           request.Endereco.Estado
        );

        var clienteSelecionado = await repositorioCliente.SelecionarPorIdAsync(request.Id);

        if (clienteSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        clienteSelecionado.Nome = request.Nome;
        clienteSelecionado.Endereco = endereco;
        clienteSelecionado.Telefone = request.Telefone;
        clienteSelecionado.ClienteTipo = request.TipoCliente;
        clienteSelecionado.Documento = request.Documento;
        clienteSelecionado.Cnh = request.Cnh;
        clienteSelecionado.Condutor = request.Condutor;

        var resultadoValidacao = 
            await validador.ValidateAsync(clienteSelecionado, cancellationToken);
        
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var clientes = await repositorioCliente.SelecionarTodosAsync();

        if (NomeDuplicado(clienteSelecionado, clientes))
            return Result.Fail(ClienteErrorResults.NomeDuplicadoError(clienteSelecionado.Nome));

        if (DocumentoDuplicado(clienteSelecionado, clientes))
            return Result.Fail(ClienteErrorResults.DocumentoDuplicadoError(clienteSelecionado.Documento));

        if(clienteSelecionado.ClienteTipo == ETipoCliente.PessoaFisica)
        {
            if (CnhDuplicado(clienteSelecionado, clientes))
                return Result.Fail(ClienteErrorResults.CnhDuplicadoError(clienteSelecionado.Cnh));
        }

        if (TelefoneDuplicado(clienteSelecionado, clientes))
            return Result.Fail(ClienteErrorResults.TelefoneDuplicadoError(clienteSelecionado.Telefone));

        if (CondutorNaoEncontrado(clienteSelecionado))
            return Result.Fail(ClienteErrorResults.CondutorNaoEncontradoError());

        try
        {
            await repositorioCliente.EditarAsync(clienteSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }
 
        return Result.Ok(new EditarClienteResponse(clienteSelecionado.Id));
    }
    
    private bool NomeDuplicado(Cliente cliente, IList<Cliente> clientes)
    {
        return clientes
            .Where(r => r.Id != cliente.Id)
            .Any(registro => string.Equals(
                registro.Nome,
                cliente.Nome,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }

    private bool DocumentoDuplicado(Cliente cliente, IList<Cliente> clientes)
    {
        return clientes
            .Where(r => r.Id != cliente.Id)
            .Any(registro => string.Equals(
                registro.Documento,
                cliente.Documento,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }

    private bool CnhDuplicado(Cliente cliente, IList<Cliente> clientes)
    {
        if (string.IsNullOrEmpty(cliente.Cnh))
            return false;
        return clientes
            .Where(r => r.Id != cliente.Id)
            .Any(registro => string.Equals(
                registro.Cnh,
                cliente.Cnh,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }

    private bool TelefoneDuplicado(Cliente cliente, IList<Cliente> clientes)
    {
        return clientes
            .Where(r => r.Id != cliente.Id)
            .Any(registro => registro.Telefone == cliente.Telefone);
    }

    private bool CondutorNaoEncontrado(Cliente cliente)
    {
        return cliente.Condutor == null;
    }
}