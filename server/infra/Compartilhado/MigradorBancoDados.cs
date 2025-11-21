using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;

public static class MigradorBancoDados
{
    public static bool AtualizarBancoDados(DbContext dbContext)
    {
        var qtdMigracoesPendentes = dbContext.Database.GetPendingMigrations().Count();

        if (qtdMigracoesPendentes == 0) return false;

        dbContext.Database.Migrate();

        return true;
    }
}

