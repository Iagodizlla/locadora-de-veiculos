using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

public class GrupoAutomovel : EntidadeBase
{
    public string Nome { get; set; }

    public GrupoAutomovel() { }

    public GrupoAutomovel(string nome) : this()
    {
        this.Nome = nome;
    }
}