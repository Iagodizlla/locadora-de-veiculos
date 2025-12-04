using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;

public class SelecionarFuncionariosRequestHandler(
    IRepositorioFuncionario repositorioFuncionario
) : IRequestHandler<SelecionarFuncionariosRequest, Result<SelecionarFuncionariosResponse>>
{
    public async Task<Result<SelecionarFuncionariosResponse>> Handle(SelecionarFuncionariosRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioFuncionario.SelecionarTodosAsync();

        var response = new SelecionarFuncionariosResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarFuncionariosDto(r.Id, r.Usuario.UserName, r.Salario, r.Admissao))
        };

        return Result.Ok(response);
    }
}