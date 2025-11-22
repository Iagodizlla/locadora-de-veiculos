using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloCondutor;

public class Condutor : EntidadeBase
{
    public string Nome { get; set; }
    public string Cnh { get; set; }
    public string Cpf { get; set; }
    public string Telefone { get; set; }
    public ECategoria Categoria { get; set; }
    public DateTimeOffset ValidadeCnh { get; set; }

    public Condutor() { }
    public Condutor(string nome, string cnh, string cpf, string telefone, ECategoria categoria, DateTimeOffset validadeCnh) : this()
    {
        Nome = nome;
        Cnh = cnh;
        Cpf = cpf;
        Telefone = telefone;
        Categoria = categoria;
        ValidadeCnh = validadeCnh;
    }
}