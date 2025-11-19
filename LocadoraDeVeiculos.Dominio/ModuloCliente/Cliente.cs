using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloCliente;

public class Cliente : EntidadeBase
{
    public string Nome { get; set; }
    public Endereco Endereco { get; set; }
    public string Telefone { get; set; }
    public ETipoCliente ClienteTipo { get; set; }
    public string Documento { get; set; }
    public string? Cpf { get; set; }
    public string? Rg { get; set; }
    public string? Cnh { get; set; }
    public string? Cnpj { get; set; }

    public Cliente() { }

    public Cliente(string nome, Endereco endereco, string telefone, ETipoCliente clienteTipo, string documento, string? cnpj, string? rg , string? cnh, string? cpf) : this()
    {
        Nome = nome;
        Endereco = endereco;
        Telefone = telefone;
        ClienteTipo = clienteTipo;
        Documento = documento;
        Cnpj = cnpj;
        Rg = rg;
        Cnh = cnh;
        Cpf = cpf;
    }
}
