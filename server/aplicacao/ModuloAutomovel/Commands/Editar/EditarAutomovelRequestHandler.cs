using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.Editar;

public class EditarAutomovelRequestHandler(
    IRepositorioGrupoAutomovel repositorioGrupoAutomovel,
    IRepositorioAluguel repositorioAluguel,
    IRepositorioAutomovel repositorioAutomovel,
    IContextoPersistencia contexto,
    IValidator<Automovel> validador
) : IRequestHandler<EditarAutomovelRequest, Result<EditarAutomovelResponse>>
{
    public async Task<Result<EditarAutomovelResponse>> Handle(EditarAutomovelRequest request, CancellationToken cancellationToken)
    {
        var automovelSelecionado = await repositorioAutomovel.SelecionarPorIdAsync(request.Id);

        if (automovelSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        if (GrupoNaoEncontrado(automovelSelecionado))
            return Result.Fail(AutomovelErrorResults.GrupoNaoEncontradoError());

        automovelSelecionado.Placa = request.Placa;
        automovelSelecionado.Modelo = request.Modelo;
        automovelSelecionado.Marca = request.Marca;
        automovelSelecionado.Cor = request.Cor;
        automovelSelecionado.Ano = request.Ano;
        automovelSelecionado.CapacidadeTanque = request.CapacidadeTanque;
        automovelSelecionado.Foto = request.Foto;
        automovelSelecionado.GrupoAutomovel = await repositorioGrupoAutomovel.SelecionarPorIdAsync(request.GrupoAutomovelId);
        automovelSelecionado.Combustivel = request.Combustivel;

        var resultadoValidacao = 
            await validador.ValidateAsync(automovelSelecionado, cancellationToken);
        
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        if (await repositorioAluguel.VeiculoEmAluguelAtivoAsync(request.Id))
            return Result.Fail(AutomovelErrorResults.VeiculoComAluguelNaoFinalizadoError());

        var automoveis = await repositorioAutomovel.SelecionarTodosAsync();

        if (PlacaDuplicado(automovelSelecionado, automoveis))
            return Result.Fail(AutomovelErrorResults.PlacaDuplicadoError(automovelSelecionado.Placa));

        try
        {
            await repositorioAutomovel.EditarAsync(automovelSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }
 
        return Result.Ok(new EditarAutomovelResponse(automovelSelecionado.Id));
    }
    
    private bool PlacaDuplicado(Automovel automovel, IList<Automovel> automoveis)
    {
        return automoveis
            .Where(r => r.Id != automovel.Id)
            .Any(registro => string.Equals(
                registro.Placa,
                automovel.Placa,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }

    private bool GrupoNaoEncontrado(Automovel automovel)
    {
        return automovel.GrupoAutomovel == null;
    }
}