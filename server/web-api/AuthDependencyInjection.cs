using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.Services;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using LocadoraDeVeiculos.WebApi.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LocadoraDeVeiculos.WebApi;

public static class AuthDependencyInjection
{
    public static void ConfigureIdentityProviders(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, JwtProvider>();
        services.AddScoped<ITenantProvider, ApiTenantProvider>();

        services.AddIdentity<Usuario, Cargo>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<LocadoraVeiculoDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var chaveAssinaturaJwt = config["JWT_GENERATION_KEY"];

        if (chaveAssinaturaJwt == null)
            throw new ArgumentException("Não foi possível obter a chave de assinatura de tokens.");

        var chaveEmBytes = Encoding.ASCII.GetBytes(chaveAssinaturaJwt);

        var audienciaValida = config["JWT_AUDIENCE_DOMAIN"];

        if (audienciaValida == null)
            throw new ArgumentException("Não foi possível obter o domínio da audiência dos tokens.");

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;

            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(chaveEmBytes),
                ValidAudience = audienciaValida,
                ValidIssuer = "LocadoraVeiculo",
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(10)
            };
        });
    }
}