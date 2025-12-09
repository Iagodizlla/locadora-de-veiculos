using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel;
using LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Editar;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;
using System.Numerics;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAluguel.Commands.Editar;

public class EditarAluguelRequestHandler(
    IRepositorioCliente repositorioCliente,
    IRepositorioCondutor repositorioCondutor,
    IRepositorioPlano repositorioPlano,
    IRepositorioAutomovel repositorioAutomovel,
    IRepositorioTaxa repositorioTaxa,
    IRepositorioAluguel repositorioAluguel,
    IContextoPersistencia contexto,
    IValidator<Aluguel> validador
) : IRequestHandler<EditarAluguelRequest, Result<EditarAluguelResponse>>
{
    public async Task<Result<EditarAluguelResponse>> Handle(EditarAluguelRequest request, CancellationToken cancellationToken)
    {
        var aluguelSelecionado = await repositorioAluguel.SelecionarPorIdAsync(request.Id);

        if (aluguelSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        if (aluguelSelecionado.Status == false)
            return Result.Fail(AluguelErrorResults.AluguelNaoPodeSerExcluidoError());

        #region Cliar entidades
        var cliente = await repositorioCliente.SelecionarPorIdAsync(request.ClienteId);
        var condutor = await repositorioCondutor.SelecionarPorIdAsync(request.CondutorId);
        var plano = await repositorioPlano.SelecionarPorIdAsync(request.PlanoId);
        var automovel = await repositorioAutomovel.SelecionarPorIdAsync(request.AutomovelId);
        var taxas = await repositorioTaxa.SelecionarTodosPorIdAsync(request.Taxas);
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
        if (await repositorioAluguel.ClienteEstaOcupadoAsync(request.ClienteId, request.Id))
            return Result.Fail(AluguelErrorResults.ClienteEmAluguelAtivoError());

        if (await repositorioAluguel.CondutorEstaOcupadoAsync(request.CondutorId, request.Id))
            return Result.Fail(AluguelErrorResults.CondutorEmAluguelAtivoError());

        if (await repositorioAluguel.AutomovelEstaOcupadoAsync(request.AutomovelId, request.Id))
            return Result.Fail(AluguelErrorResults.AutomovelEmAluguelAtivoError());
        #endregion
        #region Validacao se existe
        if (CondutorNaoEncontrado(aluguelSelecionado.Condutor))
            return Result.Fail(AluguelErrorResults.CondutorNaoEncontradoError());

        if (ClienteNaoEncontrado(aluguelSelecionado.Cliente))
            return Result.Fail(AluguelErrorResults.ClienteNaoEncontradoError());

        if (PlanoNaoEncontrado(aluguelSelecionado.Plano))
            return Result.Fail(AluguelErrorResults.PlanoNaoEncontradoError());

        if (AutomovelNaoEncontrado(aluguelSelecionado.Automovel))
            return Result.Fail(AluguelErrorResults.AutomovelNaoEncontradoError());
        #endregion

        aluguelSelecionado.Cliente = cliente;
        aluguelSelecionado.Condutor = condutor;
        aluguelSelecionado.Plano = plano;
        aluguelSelecionado.Status = true;
        aluguelSelecionado.Automovel = automovel;
        aluguelSelecionado.Taxas = taxas;
        aluguelSelecionado.DataSaida = request.DataSaida;
        aluguelSelecionado.DataRetornoPrevista = request.DataRetornoPrevista;
        aluguelSelecionado.QuilometragemInicial = request.KmInicial;
        aluguelSelecionado.SeguroCliente = request.SeguroCliente;
        aluguelSelecionado.SeguroTerceiro = request.SeguroTerceiro;
        aluguelSelecionado.ValorSeguroPorDia = request.ValorSeguroPorDia;
        aluguelSelecionado.NivelCombustivelNaSaida = request.NivelCombustivelNaSaida;
        aluguelSelecionado.ValorTotal = 1000m;

        var resultadoValidacao = 
            await validador.ValidateAsync(aluguelSelecionado, cancellationToken);
        
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var grupoAutomoveis = await repositorioAluguel.SelecionarTodosAsync();

        try
        {
            await repositorioAluguel.EditarAsync(aluguelSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }
 
        return Result.Ok(new EditarAluguelResponse(aluguelSelecionado.Id));
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