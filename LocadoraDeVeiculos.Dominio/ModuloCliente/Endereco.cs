using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloCliente;

public class Endereco : EntidadeBase
{
    public string Logradouro { get; set; }
    public int Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public Endereco() { }
    public Endereco(string logradouro, int numero, string bairro, string cidade, string estado) : this()
    {
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }
}