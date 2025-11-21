using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloCondutor;

public class Condutor : EntidadeBase
{
    public string Nome { get; set; }
    public string Cnh { get; set; }
    public ECategoria Categoria { get; set; }
    public DateTimeOffset ValidadeCnh { get; set; }

    public Condutor() { }
    public Condutor(string nome, string cnh, ECategoria categoria, DateTimeOffset validadeCnh) : this()
    {
        Nome = nome;
        Cnh = cnh;
        Categoria = categoria;
        ValidadeCnh = validadeCnh;
    }
}