using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarTodos;

public class SelecionarCondutoresRequestHandler(
    IRepositorioCondutor repositorioCondutor
) : IRequestHandler<SelecionarCondutoresRequest, Result<SelecionarCondutoresResponse>>
{
    public async Task<Result<SelecionarCondutoresResponse>> Handle(SelecionarCondutoresRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioCondutor.SelecionarTodosAsync();

        var response = new SelecionarCondutoresResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarCondutoresDto(r.Id, r.Nome, r.Cnh, r.Cpf, r.Telefone, r.Categoria, r.ValidadeCnh))
        };

        return Result.Ok(response);
    }
}