namespace LocadoraDeVeiculos.Dominio.ModuloCliente;

public class PessoaFisica
{
    public string Cpf { get; set; }
    public string Rg { get; set; }
    public string Cnh { get; set; }

    public PessoaFisica() { }

    public PessoaFisica(string cpf, string rg, string cnh) : this()
    {
        Cpf = cpf;
        Rg = rg;
        Cnh = cnh;
    }
}
