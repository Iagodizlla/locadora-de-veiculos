using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAluguel;

public class MapeadorAluguelEmOrm : IEntityTypeConfiguration<Aluguel>
{
    public void Configure(EntityTypeBuilder<Aluguel> modelBuilder)
    {
        modelBuilder.ToTable("TBAluguel");

        modelBuilder.Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.HasOne(a => a.Cliente)
            .WithMany()
            .HasForeignKey(a => a.ClienteId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.HasOne(a => a.Condutor)
           .WithMany()
           .HasForeignKey(a => a.CondutorId)
           .IsRequired()
           .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.HasOne(a => a.Automovel)
           .WithMany()
           .HasForeignKey(a => a.AutomovelId)
           .IsRequired()
           .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.HasOne(a => a.Plano)
           .WithMany()
           .HasForeignKey(a => a.PlanoId)
           .IsRequired()
           .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .HasMany(a => a.Taxas)
            .WithMany() 
            .UsingEntity(j => j.ToTable("TBAluguelTaxa"));

        modelBuilder.Property(a => a.DataSaida)
            .IsRequired()
            .HasColumnType("datetimeoffset");

        modelBuilder.Property(a => a.DataRetornoPrevista)
            .IsRequired()
            .HasColumnType("datetimeoffset");

        modelBuilder.Property(a => a.DataDevolucao)
            .IsRequired(false)
            .HasColumnType("datetimeoffset");

        modelBuilder.Property(a => a.QuilometragemInicial)
            .IsRequired()
            .HasColumnType("int");

        modelBuilder.Property(a => a.QuilometragemFinal)
            .IsRequired(false)
            .HasColumnType("int");

        modelBuilder.Property(a => a.NivelCombustivelNaSaida)
            .IsRequired()
            .HasColumnType("int");

        modelBuilder.Property(a => a.NivelCombustivelNaDevolucao)
            .IsRequired(false)
            .HasColumnType("int");

        modelBuilder.Property(a => a.SeguroCliente)
            .IsRequired()
            .HasColumnType("bit");

        modelBuilder.Property(a => a.SeguroTerceiro)
            .IsRequired()
            .HasColumnType("bit");

        modelBuilder.Property(b => b.ValorSeguroPorDia)
            .HasColumnType("float")
            .IsRequired(false);

        modelBuilder.Property(a => a.Status)
            .IsRequired()
            .HasColumnType("bit");

        modelBuilder
            .HasOne(a => a.Empresa)
            .WithMany()
            .HasForeignKey(a => a.EmpresaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
