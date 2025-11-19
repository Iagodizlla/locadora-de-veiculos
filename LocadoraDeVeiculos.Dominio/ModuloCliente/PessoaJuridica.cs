namespace LocadoraDeVeiculos.Dominio.ModuloCliente;

public class PessoaJuridica
{
    public string Cnpj { get; set; }
    public PessoaFisica RepresentanteLegal { get; set; }

    public PessoaJuridica() { }
    public PessoaJuridica(string cnpj, PessoaFisica representanteLegal) : this()
    {
        Cnpj = cnpj;
        RepresentanteLegal = representanteLegal;
    }
}