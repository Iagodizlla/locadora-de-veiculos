using LocadoraDeVeiculos.Dominio.ModuloConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloConfig;
public class MapeadorConfigEmOrm : IEntityTypeConfiguration<Config>
{
    public void Configure(EntityTypeBuilder<Config> modelBuilder)
    {
        modelBuilder.ToTable("TBConfig");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Property(x => x.Gasolina)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        modelBuilder.Property(x => x.Gas)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        modelBuilder.Property(x => x.Diessel)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        modelBuilder.Property(x => x.Alcool)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        modelBuilder
            .HasOne(a => a.Empresa)
            .WithMany()
            .HasForeignKey(a => a.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}