using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloGrupoAutomovel;

public class MapeadorGrupoAutomovelEmOrm : IEntityTypeConfiguration<GrupoAutomovel>
{
    public void Configure(EntityTypeBuilder<GrupoAutomovel> modelBuilder)
    {
        modelBuilder.ToTable("TBGrupoAutomovel");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Property(b => b.Nome)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        modelBuilder
            .HasOne(a => a.Empresa)
            .WithMany()
            .HasForeignKey(a => a.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
