using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Excluir;

public class ExcluirFuncionarioRequestHandler(
    IRepositorioFuncionario repositorioFuncionario,
    IContextoPersistencia contexto,
    UserManager<Usuario> userManager
) : IRequestHandler<ExcluirFuncionarioRequest, Result<ExcluirFuncionarioResponse>>
{
    public async Task<Result<ExcluirFuncionarioResponse>> Handle(ExcluirFuncionarioRequest request, CancellationToken cancellationToken)
    {
        var funcionarioSelecionado = await repositorioFuncionario.SelecionarPorIdAsync(request.Id);

        if (funcionarioSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        try
        {
            await repositorioFuncionario.ExcluirAsync(funcionarioSelecionado);

            if (funcionarioSelecionado.UsuarioId.HasValue)
            {
                var usuario = await userManager.FindByIdAsync(funcionarioSelecionado.UsuarioId.Value.ToString());
                if (usuario != null)
                {
                    var resultadoUsuario = await userManager.DeleteAsync(usuario);
                    if (!resultadoUsuario.Succeeded)
                    {
                        await contexto.RollbackAsync();
                        var erros = resultadoUsuario.Errors.Select(e => e.Description).ToList();
                        return Result.Fail(ErrorResults.BadRequestError(erros));
                    }
                }
            }

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirFuncionarioResponse());
    }
}