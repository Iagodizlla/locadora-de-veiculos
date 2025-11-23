using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloTaxa;

public class MapeadorTaxaEmOrm : IEntityTypeConfiguration<Taxa>
{
    public void Configure(EntityTypeBuilder<Taxa> modelBuilder)
    {
        modelBuilder.ToTable("TBTaxa");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Property(b => b.Nome)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        modelBuilder.Property(b => b.Preco)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        modelBuilder.Property(b => b.Servico)
            .HasColumnType("int")
            .IsRequired();

        modelBuilder
            .HasOne(a => a.Empresa)
            .WithMany()
            .HasForeignKey(a => a.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
