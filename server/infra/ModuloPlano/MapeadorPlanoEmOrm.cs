using LocadoraDeVeiculos.Dominio.ModuloPlano;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloPlano;

public class MapeadorPlanoEmOrm : IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> modelBuilder)
    {
        modelBuilder.ToTable("TBPlano");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Property(b => b.PrecoDiario)
            .HasColumnType("float")
            .IsRequired();

        modelBuilder.Property(b => b.PrecoDiarioControlado)
            .HasColumnType("float")
            .IsRequired();

        modelBuilder.Property(b => b.PrecoPorKm)
            .HasColumnType("float")
            .IsRequired();

        modelBuilder.Property(b => b.KmLivres)
            .HasColumnType("float")
            .IsRequired();

        modelBuilder.Property(b => b.PrecoPorKmExplorado)
            .HasColumnType("float")
            .IsRequired();

        modelBuilder.Property(b => b.PrecoLivre)
            .HasColumnType("float")
            .IsRequired();

        modelBuilder.HasOne(a => a.GrupoAutomovel)
            .WithMany()
            .HasForeignKey(a => a.GrupoAutomovelId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .HasOne(a => a.Empresa)
            .WithMany()
            .HasForeignKey(a => a.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}