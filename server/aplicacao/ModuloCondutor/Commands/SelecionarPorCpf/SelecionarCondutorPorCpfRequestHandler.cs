using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarPorCpf;

public class SelecionarCondutorPorCpfRequestHandler(
    IRepositorioCondutor repositorioCondutor
) : IRequestHandler<SelecionarCondutorPorCpfRequest, Result<SelecionarCondutorPorCpfResponse>>
{
    public async Task<Result<SelecionarCondutorPorCpfResponse>> Handle(SelecionarCondutorPorCpfRequest request, CancellationToken cancellationToken)
    {
        var condutorSelecionado = await repositorioCondutor.SelecionarPorCpfAsync(request.Cpf);

        if (condutorSelecionado is null)
            return Result.Fail(CpfNaoEncontrado(request.Cpf));

        var resposta = new SelecionarCondutorPorCpfResponse(
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
    private static Error CpfNaoEncontrado(string cpf)
    {
        return new Error("Registro não encontrado")
            .CausedBy($"Não foi possível encontrar o registro com o CPF {cpf}")
            .WithMetadata("ErrorType", "NotFound");
    }
}