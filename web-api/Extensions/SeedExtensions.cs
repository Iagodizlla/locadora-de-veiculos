using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace LocadoraDeVeiculos.WebApi.Extensions;

public static class SeedExtensions
{
    public static async Task IdentitySeederAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        ILogger logger = scope.ServiceProvider
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("IdentitySeeder");

        IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        RoleManager<Cargo> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Cargo>>();

        foreach (string role in Enum.GetNames(typeof(ECargo)))
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                IdentityResult createRole = await roleManager.CreateAsync(new Cargo { Name = role });

                if (!createRole.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Falha ao criar role '{role}': {string.Join("; ", createRole.Errors.Select(e => e.Description))}");
                }

                await roleManager.CreateAsync(new Cargo { Name = role });
            }
        }
    }
}