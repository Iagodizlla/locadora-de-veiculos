using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;

namespace LocadoraDeVeiculos.Dominio.ModuloCondutor;

public class Condutor : EntidadeBase
{
    public string Nome { get; set; }
    public string Cnh { get; set; }
    public string Cpf { get; set; }
    public string Telefone { get; set; }
    public ECategoria Categoria { get; set; }
    public DateTimeOffset ValidadeCnh { get; set; }
    public Cliente? Cliente { get; set; }
    public Guid? ClienteId { get; set; }
    public bool ECliente { get; set; }

    public Condutor() { }
    public Condutor(string nome, string cnh, string cpf, string telefone, ECategoria categoria, DateTimeOffset validadeCnh, Cliente? cliente, bool eCliente) : this()
    {
        Nome = nome;
        Cnh = cnh;
        Cpf = cpf;
        Telefone = telefone;
        Categoria = categoria;
        ValidadeCnh = validadeCnh;
        Cliente = cliente;
        ECliente = eCliente;
    }
}