using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Inserir;

public class InserirAluguelRequestHandler(
    IRepositorioCondutor repositorioCondutor,
    IRepositorioCliente repositorioCliente,
    IRepositorioAutomovel repositorioAutomovel,
    IRepositorioPlano repositorioPlano,
    IRepositorioTaxa repositorioTaxa,
    IContextoPersistencia contexto,
    IRepositorioAluguel repositorioAluguel,
    ITenantProvider tenantProvider,
    IValidator<Aluguel> validador
) : IRequestHandler<InserirAluguelRequest, Result<InserirAluguelResponse>>
{
    public async Task<Result<InserirAluguelResponse>> Handle(
        InserirAluguelRequest request, CancellationToken cancellationToken)
    {
        #region Entidades
        var cliente = await repositorioCliente.SelecionarPorIdAsync(request.ClienteId);
        var condutor = await repositorioCondutor.SelecionarPorIdAsync(request.CondutorId);
        var plano = await repositorioPlano.SelecionarPorIdAsync(request.PlanoId);
        var automovel = await repositorioAutomovel.SelecionarPorIdAsync(request.AutomovelId);
        var taxas = await repositorioTaxa.SelecionarTodosPorIdAsync(request.TaxasId);
        #endregion
        #region Validacao de encontrar
        if (ClienteNaoEncontrado(cliente))
            return Result.Fail(AluguelErrorResults.ClienteNaoEncontradoError());
        if (CondutorNaoEncontrado(condutor))
            return Result.Fail(AluguelErrorResults.CondutorNaoEncontradoError());
        if (PlanoNaoEncontrado(plano))
            return Result.Fail(AluguelErrorResults.PlanoNaoEncontradoError());
        if (AutomovelNaoEncontrado(automovel))
            return Result.Fail(AluguelErrorResults.AutomovelNaoEncontradoError());
        #endregion
        #region Validacao de sequencia
        if (cliente.ClienteTipo == ETipoCliente.PessoaJuridica && condutor.ECliente == true)
            return Result.Fail(AluguelErrorResults.CondutorInvalidoParaClientePJError());
        if (cliente.ClienteTipo == ETipoCliente.PessoaFisica && condutor.ECliente == false)
            return Result.Fail(AluguelErrorResults.CondutorInvalidoParaClientePFError());
        if (automovel.GrupoAutomovel.Id != plano.GrupoAutomovel.Id)
            return Result.Fail(AluguelErrorResults.GruposAutomovelIncompativeisError());
        #endregion
        #region Validacao de disponiveis
        if (await repositorioAluguel.ClienteEstaOcupadoAsync(request.ClienteId))
            return Result.Fail(AluguelErrorResults.ClienteEmAluguelAtivoError());
        if (await repositorioAluguel.CondutorEstaOcupadoAsync(request.CondutorId))
            return Result.Fail(AluguelErrorResults.CondutorEmAluguelAtivoError());
        if (await repositorioAluguel.AutomovelEstaOcupadoAsync(request.AutomovelId))
            return Result.Fail(AluguelErrorResults.AutomovelEmAluguelAtivoError());
        #endregion

        var grupoAutomovel = new Aluguel(cliente, condutor, automovel, plano, taxas, request.DataSaisa, request.DataRetornoPrevista,
            request.DataDevolucao, request.KmInicial, request.KmFinal, request.NivelCombustivelNaSaida, request.NivelCombustivelNaDevolucao,
            request.SeguroCliente, request.SeguroTerceiro, request.ValorSeguroPorDia, request.Status, request.valorTotal)
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


        // inserção
        try
        {
            await repositorioAluguel.InserirAsync(grupoAutomovel);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new InserirAluguelResponse(grupoAutomovel.Id));
    }

    private bool CondutorNaoEncontrado(Condutor condutor)
    {
        return condutor == null;
    }
    private bool ClienteNaoEncontrado(Cliente cliente)
    {
        return cliente == null;
    }
    private bool PlanoNaoEncontrado(Plano plano)
    {
        return plano == null;
    }
    private bool AutomovelNaoEncontrado(Automovel automovel)
    {
        return automovel == null;
    }
}
