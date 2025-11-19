using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAutomovel;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCondutor;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloFuncionario;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloGrupoAutomovel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;

public class LocadoraVeiculoDbContext(DbContextOptions options, ITenantProvider? tenantProvider = null)
    : IdentityDbContext<Usuario, Cargo, Guid>(options), IContextoPersistencia
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (tenantProvider is not null)
        {
            modelBuilder.Entity<Funcionario>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
            modelBuilder.Entity<GrupoAutomovel>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
            //modelBuilder.Entity<Plano>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
            modelBuilder.Entity<Automovel>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
            //modelBuilder.Entity<Cliente>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
            modelBuilder.Entity<Condutor>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
            //modelBuilder.Entity<Taxa>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
            //modelBuilder.Entity<Aluguel>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
            //modelBuilder.Entity<Configuracao>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
        }

        modelBuilder.ApplyConfiguration(new MapeadorFuncionarioEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorGrupoAutomovelEmOrm());
        //modelBuilder.ApplyConfiguration(new MapeadorPlanoEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorAutomovelEmOrm());
        //modelBuilder.ApplyConfiguration(new MapeadorClienteEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorCondutorEmOrm());
        //modelBuilder.ApplyConfiguration(new MapeadorTaxaEmOrm());
        //modelBuilder.ApplyConfiguration(new MapeadorAluguelEmOrm());
        //modelBuilder.ApplyConfiguration(new MapeadorConfiguracaoEmOrm());

        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> GravarAsync()
    {
        return await SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }

        await Task.CompletedTask;
    }
}

