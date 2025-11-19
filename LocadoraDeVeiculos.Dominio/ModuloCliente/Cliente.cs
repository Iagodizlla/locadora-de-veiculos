using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloCliente;

public class Cliente : EntidadeBase
{
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Telefone { get; set; }
    public PessoaJuridica? PessoaJuridica { get; set; }
    public PessoaFisica? PessoaFisica { get; set; }

    public Cliente() { }

    public Cliente(string nome, string endereco, string telefone, PessoaJuridica? pessoaJuridica, PessoaFisica? pessoaFisica) : this()
    {
        Nome = nome;
        Endereco = endereco;
        Telefone = telefone;
        PessoaJuridica = pessoaJuridica;
        PessoaFisica = pessoaFisica;
    }
}