using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LocadoraDeVeiculos.WebApi.Config;

public static class DatabaseConfig
{
    public static bool AutoMigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<LocadoraVeiculoDbContext>();

        var migracaoConcluida = MigradorBancoDados.AtualizarBancoDados(dbContext);

        return migracaoConcluida;
    }
}
