using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloFuncionario;

public class FuncionarioFuncionarioEmOrm : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> modelBuilder)
    {
        modelBuilder.ToTable("TBFuncionario");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Property(b => b.Nome)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        modelBuilder.Property(b => b.Salario)
            .HasColumnType("float")
            .IsRequired();

        modelBuilder.Property(b => b.Admissao)
            .HasColumnType("datetimeoffset")
            .IsRequired();

        modelBuilder
            .HasOne(a => a.Usuario)
            .WithMany()
            .HasForeignKey(a => a.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}