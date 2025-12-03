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
    RoleManager<Cargo> roleManager,
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
        var cargoStr = ECargo.Funcionario.ToString(); // Funcionario ou Empresa

        var resultadoBuscaCargo = await roleManager.FindByNameAsync(cargoStr);

        if (resultadoBuscaCargo is null)
        {
            var novoCargo = new Cargo()
            {
                Name = cargoStr,
                NormalizedName = cargoStr.ToUpper()
            };

            var resultCriarCargo = await roleManager.CreateAsync(novoCargo);

            if (!resultCriarCargo.Succeeded)
            {
                var erros = resultCriarCargo.Errors.Select(x => x.Description).ToList();
                return Result.Fail(ErrorResults.BadRequestError(erros));
            }

            resultadoBuscaCargo = novoCargo;
        }

        var resultadoInclusaoCargo =  await userManager.AddToRoleAsync(usuario, resultadoBuscaCargo.Name);

        if (!resultadoInclusaoCargo.Succeeded)
        {
            var erros = resultadoInclusaoCargo.Errors.Select(e => e.Description);
        }

        usuario.AssociarEmpresa(usuario.Id);

        var configuracaoInicial = new Config
        {
            EmpresaId = usuario.EmpresaId,
            Gasolina = 0m,
            Gas = 0m,
            Diesel = 0m,
            Alcool = 0m
        };

        await repositorioConfig.InserirAsync(configuracaoInicial);

        await contexto.GravarAsync();

        await userManager.AddToRoleAsync(usuario, "Empresa");

        var tokenAcesso = await tokenProvider.GerarTokenDeAcessoAsync(usuario) as TokenResponse;

        if (tokenAcesso == null)
            return Result.Fail(ErrorResults.InternalServerError(new Exception("Falha ao gerar token de acesso")));

        return Result.Ok(tokenAcesso);
    }
}
