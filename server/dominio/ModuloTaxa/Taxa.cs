using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;

namespace LocadoraDeVeiculos.Dominio.ModuloTaxa;

public class Taxa : EntidadeBase
{
    public string Nome { get; set; }
    public double Preco { get; set; }
    public EServico Servico { get; set; }

    public Taxa() { }
    public Taxa(string nome, double preco, EServico servico) : this()
    {
        Nome = nome;
        Preco = preco;
        Servico = servico;
    }
}
