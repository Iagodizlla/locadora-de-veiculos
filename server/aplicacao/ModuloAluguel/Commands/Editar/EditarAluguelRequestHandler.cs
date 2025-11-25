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
using MediatR;
using System.Numerics;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAluguel.Commands.Editar;

public class EditarAluguelRequestHandler(
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

        #region Validacao de sequencia
        if (request.Cliente.ClienteTipo == ETipoCliente.PessoaJuridica && request.Condutor.ECliente == true)
            return Result.Fail(AluguelErrorResults.CondutorInvalidoParaClientePJError());
        if (request.Cliente.ClienteTipo == ETipoCliente.PessoaFisica && request.Condutor.ECliente == false)
            return Result.Fail(AluguelErrorResults.CondutorInvalidoParaClientePFError());
        if (request.Automovel.GrupoAutomovel.Id != request.Plano.GrupoAutomovel.Id)
            return Result.Fail(AluguelErrorResults.GruposAutomovelIncompativeisError());
        #endregion
        #region Validacao de disponiveis
        if (await repositorioAluguel.ClienteEstaOcupadoAsync(request.Cliente.Id))
            return Result.Fail(AluguelErrorResults.ClienteEmAluguelAtivoError());
        if (await repositorioAluguel.CondutorEstaOcupadoAsync(request.Condutor.Id))
            return Result.Fail(AluguelErrorResults.CondutorEmAluguelAtivoError());
        if (await repositorioAluguel.AutomovelEstaOcupadoAsync(request.Automovel.Id))
            return Result.Fail(AluguelErrorResults.AutomovelEmAluguelAtivoError());
        #endregion

        aluguelSelecionado.Cliente = request.Cliente;
        aluguelSelecionado.Condutor = request.Condutor;
        aluguelSelecionado.Plano = request.Plano;
        aluguelSelecionado.Status = request.Status;
        aluguelSelecionado.Automovel = request.Automovel;
        aluguelSelecionado.Taxas = request.Taxas;
        aluguelSelecionado.DataSaida = request.DataSaisa;
        aluguelSelecionado.DataRetornoPrevista = request.DataRetornoPrevista;
        aluguelSelecionado.DataDevolucao = request.DataDevolucao;
        aluguelSelecionado.QuilometragemInicial = request.KmInicial;
        aluguelSelecionado.QuilometragemFinal = request.KmFianl;
        aluguelSelecionado.SeguroCliente = request.SeguroCliente;
        aluguelSelecionado.SeguroTerceiro = request.SeguroTerceiro;
        aluguelSelecionado.ValorSeguroPorDia = request.ValorSeguroPorDia;
        aluguelSelecionado.NivelCombustivelNaSaida = request.NivelCombustivelNaSaida;
        aluguelSelecionado.NivelCombustivelNaDevolucao = request.NivelCombustivelNaDevolucao;

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

        if (CondutorNaoEncontrado(aluguelSelecionado.Condutor))
            return Result.Fail(AluguelErrorResults.CondutorNaoEncontradoError());

        if (ClienteNaoEncontrado(aluguelSelecionado.Cliente))
            return Result.Fail(AluguelErrorResults.ClienteNaoEncontradoError());

        if (PlanoNaoEncontrado(aluguelSelecionado.Plano))
            return Result.Fail(AluguelErrorResults.PlanoNaoEncontradoError());

        if (AutomovelNaoEncontrado(aluguelSelecionado.Automovel))
            return Result.Fail(AluguelErrorResults.AutomovelNaoEncontradoError());

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