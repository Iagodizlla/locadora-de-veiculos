using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Inserir;

public class InserirClienteRequestHandler(
    IRepositorioCondutor repositorioCondutor,
    UserManager<Usuario> userManager,
    IContextoPersistencia contexto,
    IRepositorioCliente repositorioCliente,
    ITenantProvider tenantProvider,
    IValidator<Cliente> validador
) : IRequestHandler<InserirClienteRequest, Result<InserirClienteResponse>>
{
    public async Task<Result<InserirClienteResponse>> Handle(
        InserirClienteRequest request, CancellationToken cancellationToken)
    {
        var endereco = new Endereco(
            request.Endereco.Logradouro,
            request.Endereco.Numero,
            request.Endereco.Bairro,
            request.Endereco.Cidade,
            request.Endereco.Estado
        )
        {
            EmpresaId = tenantProvider.EmpresaId.GetValueOrDefault()
        };

        var condutor = await repositorioCondutor.SelecionarPorIdAsync(request.CondutorId.GetValueOrDefault());

        if (CondutorNaoEncontrado(condutor))
            return Result.Fail(ClienteErrorResults.CondutorNaoEncontradoError());

        var cliente = new Cliente(request.Nome, endereco, request.Telefone, request.TipoCliente, request.Documento, request.Cnh, condutor)
        {
            EmpresaId = tenantProvider.EmpresaId.GetValueOrDefault()
        };

        // validações
        var resultadoValidacao = await validador.ValidateAsync(cliente);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
               .Select(failure => failure.ErrorMessage)
               .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var clientesRegistrados = await repositorioCliente.SelecionarTodosAsync();

        if (NomeDuplicado(cliente, clientesRegistrados))
            return Result.Fail(ClienteErrorResults.NomeDuplicadoError(cliente.Nome));

        if (DocumentoDuplicado(cliente, clientesRegistrados))
            return Result.Fail(ClienteErrorResults.DocumentoDuplicadoError(cliente.Documento));

        if (cliente.ClienteTipo == ETipoCliente.PessoaFisica)
        {
            if (CnhDuplicado(cliente, clientesRegistrados))
                return Result.Fail(ClienteErrorResults.CnhDuplicadoError(cliente.Cnh));
        }

        if (TelefoneDuplicado(cliente, clientesRegistrados))
            return Result.Fail(ClienteErrorResults.TelefoneDuplicadoError(cliente.Telefone));

        // inserção
        try
        {
            await repositorioCliente.InserirAsync(cliente);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new InserirClienteResponse(cliente.Id));
    }

    private bool NomeDuplicado(Cliente cliente, IList<Cliente> clientes)
    {
        return clientes
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

    private bool CondutorNaoEncontrado(Condutor condutor)
    {
        return condutor == null;
    }
}
