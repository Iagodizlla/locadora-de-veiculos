using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarPorId;

public class SelecionarAutomovelPorIdRequestHandler(
    IRepositorioAutomovel repositorioAutomovel
) : IRequestHandler<SelecionarAutomovelPorIdRequest, Result<SelecionarAutomovelPorIdResponse>>
{
    public async Task<Result<SelecionarAutomovelPorIdResponse>> Handle(SelecionarAutomovelPorIdRequest request, CancellationToken cancellationToken)
    {
        var automovelSelecionado = await repositorioAutomovel.SelecionarPorIdAsync(request.Id);

        if (automovelSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        var resposta = new SelecionarAutomovelPorIdResponse(
            automovelSelecionado.Id,
            automovelSelecionado.Placa,
            automovelSelecionado.Modelo,
            automovelSelecionado.Marca,
            automovelSelecionado.Cor,
            automovelSelecionado.Ano,
            automovelSelecionado.CapacidadeTanque,
            automovelSelecionado.Foto,
            automovelSelecionado.GrupoAutomovel,
            automovelSelecionado.Combustivel
        );

        return Result.Ok(resposta);
    }
}