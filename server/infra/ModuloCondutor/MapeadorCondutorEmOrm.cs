using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCondutor;

public class MapeadorCondutorEmOrm : IEntityTypeConfiguration<Condutor>
{
    public void Configure(EntityTypeBuilder<Condutor> modelBuilder)
    {
        modelBuilder.ToTable("TBCondutor");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        modelBuilder.Property(c => c.Cnh)
            .IsRequired()
            .HasColumnType("nvarchar(20)");

        modelBuilder.Property(c => c.Cpf)
            .IsRequired()
            .HasColumnType("nvarchar(14)");

        modelBuilder.Property(c => c.Telefone)
            .IsRequired()
            .HasColumnType("nvarchar(20)");

        modelBuilder.Property(c => c.Categoria)
            .IsRequired()
            .HasColumnType("int");

        modelBuilder.Property(c => c.ValidadeCnh)
            .IsRequired()
            .HasColumnType("datetimeoffset");

        modelBuilder
            .HasOne(a => a.Empresa)
            .WithMany()
            .HasForeignKey(a => a.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}