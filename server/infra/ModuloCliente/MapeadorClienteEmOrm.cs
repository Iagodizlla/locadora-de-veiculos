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

        modelBuilder.OwnsOne(c => c.Endereco, endereco =>
        {
            endereco.Property(e => e.Logradouro).HasColumnName("Logradouro");
            endereco.Property(e => e.Numero).HasColumnName("Numero");
            endereco.Property(e => e.Bairro).HasColumnName("Bairro");
            endereco.Property(e => e.Cidade).HasColumnName("Cidade");
            endereco.Property(e => e.Estado).HasColumnName("Estado");
        });

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
