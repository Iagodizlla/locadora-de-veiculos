using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarPorId;

public class SelecionarCondutorPorIdRequestHandler(
    IRepositorioCondutor repositorioCondutor
) : IRequestHandler<SelecionarCondutorPorIdRequest, Result<SelecionarCondutorPorIdResponse>>
{
    public async Task<Result<SelecionarCondutorPorIdResponse>> Handle(SelecionarCondutorPorIdRequest request, CancellationToken cancellationToken)
    {
        var condutorSelecionado = await repositorioCondutor.SelecionarPorIdAsync(request.Id);

        if (condutorSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        var resposta = new SelecionarCondutorPorIdResponse(
            condutorSelecionado.Id,
            condutorSelecionado.Nome,
            condutorSelecionado.Cnh,
            condutorSelecionado.Cpf,
            condutorSelecionado.Telefone,
            condutorSelecionado.Categoria,
            condutorSelecionado.ValidadeCnh,
            condutorSelecionado.ECliente
        );

        return Result.Ok(resposta);
    }
}