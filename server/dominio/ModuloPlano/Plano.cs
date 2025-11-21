using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Dominio.ModuloPlano;

public class Plano : EntidadeBase
{
    public ETipoPlano TipoPlano { get; set; }
    public GrupoAutomovel GrupoAutomovel { get; set; }
    public double? PrecoDiario { get; set; }
    public double? PrecoPorKm { get; set; }
    public double? KmLivres { get; set; }
    public double? PrecoPorKmExplorado { get; set; }
    public double? PrecoLivre { get; set; }
    public Guid GrupoAutomovelId { get; set; }

    public Plano() { }
    public Plano(ETipoPlano tipoPlano, GrupoAutomovel grupoAutomovel, double? precoDiaria, double? precoPorKm, double? kmLivres, double? precoPorKmExplorado, double? precoLivre) : this()
    {
        TipoPlano = tipoPlano;
        GrupoAutomovel = grupoAutomovel;
        PrecoDiario = precoDiaria;
        PrecoPorKm = precoPorKm;
        KmLivres = kmLivres;
        PrecoPorKmExplorado = precoPorKmExplorado;
        PrecoLivre = precoLivre;
    }

}
