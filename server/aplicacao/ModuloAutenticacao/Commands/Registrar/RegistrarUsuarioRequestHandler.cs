using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.DTOs;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloConfig;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.Commands.Registrar;

public class RegistrarUsuarioRequestHandler(
    IRepositorioConfig repositorioConfig,
    IContextoPersistencia contexto,
    UserManager<Usuario> userManager,
    ITokenProvider tokenProvider
) : IRequestHandler<RegistrarUsuarioRequest, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(
        RegistrarUsuarioRequest request, CancellationToken cancellationToken)
    {
        var usuario = new Usuario
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var usuarioResult = await userManager.CreateAsync(usuario, request.Password);

        if (!usuarioResult.Succeeded)
        {
            var erros = usuarioResult
                .Errors
                .Select(failure => failure.Description)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        usuario.AssociarEmpresa(usuario.Id);

        var configuracaoInicial = new Config
        {
            EmpresaId = usuario.EmpresaId,
            Gasolina = 0m,
            Gas = 0m,
            Diessel = 0m,
            Alcool = 0m
        };

        await repositorioConfig.InserirAsync(configuracaoInicial);

        await contexto.GravarAsync();

        await userManager.AddToRoleAsync(usuario, "Adm");

        var tokenAcesso = tokenProvider.GerarTokenDeAcesso(usuario) as TokenResponse;

        if (tokenAcesso == null)
            return Result.Fail(ErrorResults.InternalServerError(new Exception("Falha ao gerar token de acesso")));

        return Result.Ok(tokenAcesso);
    }
}
