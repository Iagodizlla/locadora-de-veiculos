using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloConfig;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAluguel;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAutomovel;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCliente;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCondutor;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloConfig;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloFuncionario;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloPlano;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloTaxa;
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
            modelBuilder.Entity<Funcionario>().HasQueryFilter(m => m.UsuarioId == tenantProvider.EmpresaId);
            modelBuilder.Entity<GrupoAutomovel>().HasQueryFilter(m => m.EmpresaId == tenantProvider.EmpresaId);
            modelBuilder.Entity<Plano>().HasQueryFilter(m => m.EmpresaId == tenantProvider.EmpresaId);
            modelBuilder.Entity<Automovel>().HasQueryFilter(m => m.EmpresaId == tenantProvider.EmpresaId);
            modelBuilder.Entity<Cliente>().HasQueryFilter(m => m.EmpresaId == tenantProvider.EmpresaId);
            modelBuilder.Entity<Condutor>().HasQueryFilter(m => m.EmpresaId == tenantProvider.EmpresaId);
            modelBuilder.Entity<Taxa>().HasQueryFilter(m => m.EmpresaId == tenantProvider.EmpresaId);
            modelBuilder.Entity<Aluguel>().HasQueryFilter(m => m.EmpresaId == tenantProvider.EmpresaId);
            modelBuilder.Entity<Config>().HasQueryFilter(m => m.EmpresaId == tenantProvider.EmpresaId);
        }

        modelBuilder.ApplyConfiguration(new MapeadorFuncionarioEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorGrupoAutomovelEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorPlanoEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorAutomovelEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorClienteEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorCondutorEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorTaxaEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorAluguelEmOrm());
        modelBuilder.ApplyConfiguration(new MapeadorConfigEmOrm());

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

