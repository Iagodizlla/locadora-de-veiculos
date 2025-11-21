using LocadoraDeVeiculos.Dominio.ModuloCliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCliente;

public class MapeadorClienteEmOrm : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> modelBuilder)
    {
        modelBuilder.ToTable("TBCliente");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Property(b => b.Nome)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        modelBuilder.HasOne(a => a.Endereco)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Property(b => b.Telefone)
            .HasColumnType("nvarchar(20)")
            .IsRequired();

        modelBuilder.Property(b => b.ClienteTipo)
            .HasColumnType("int")
            .IsRequired();

        modelBuilder.Property(b => b.Documento)
            .HasColumnType("nvarchar(20)")
            .IsRequired();

        modelBuilder.Property(b => b.Cnh)
            .HasColumnType("nvarchar(20)");

        modelBuilder
            .HasOne(a => a.Empresa)
            .WithMany()
            .HasForeignKey(a => a.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
