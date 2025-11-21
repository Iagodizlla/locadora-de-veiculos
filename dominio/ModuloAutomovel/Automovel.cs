using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Dominio.ModuloAutomovel;

public class Automovel : EntidadeBase
{
    public string Placa { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Cor { get; set; }
    public int Ano { get; set; }
    public int CapacidadeTanque { get; set; }
    public GrupoAutomovel GrupoAutomovel { get; set; }
    public string Foto { get; set; }
    public ECombustivel Combustivel { get; set; }

    public Guid GrupoAutomovelId { get; set; }

    public Automovel() { }

    public Automovel(string placa, string marca, string modelo, string cor, int ano, int capacidadeTanque, GrupoAutomovel grupoAutomovel, string foto, ECombustivel combustivel) : this()
    {
        Placa = placa;
        Marca = marca;
        Modelo = modelo;
        Cor = cor;
        Ano = ano;
        CapacidadeTanque = capacidadeTanque;
        GrupoAutomovel = grupoAutomovel;
        Foto = foto;
        Combustivel = combustivel;
    }
}