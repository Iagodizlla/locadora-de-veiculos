using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAutomovel;

public class MapeadorAutomovelEmOrm : IEntityTypeConfiguration<Automovel>
{
    public void Configure(EntityTypeBuilder<Automovel> modelBuilder)
    {
        modelBuilder.ToTable("TBAutomovel");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Property(a => a.Placa)
            .IsRequired()
            .HasColumnType("nvarchar(20)");

        modelBuilder.Property(a => a.Modelo)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        modelBuilder.Property(a => a.Marca)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        modelBuilder.Property(a => a.Cor)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        modelBuilder.Property(a => a.Ano)
            .IsRequired()
            .HasColumnType("int");

        modelBuilder.Property(a => a.CapacidadeTanque)
            .IsRequired()
            .HasColumnType("int");

        modelBuilder.Property(a => a.Foto)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        modelBuilder.Property(a => a.Combustivel)
            .IsRequired()
            .HasColumnType("int");

        modelBuilder.HasOne(a => a.GrupoAutomovel)
            .WithMany()
            .HasForeignKey(a => a.GrupoAutomovelId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .HasOne(a => a.Usuario)
            .WithMany()
            .HasForeignKey(a => a.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}