using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Dominio.ModuloCliente;

public class Cliente : EntidadeBase
{
    public string Nome { get; set; }
    public Endereco Endereco { get; set; }
    public string Telefone { get; set; }
    public ETipoCliente ClienteTipo { get; set; }
    public string Documento { get; set; }
    public string? Cnh { get; set; }

    public Cliente() { }

    public Cliente(string nome, Endereco endereco, string telefone, ETipoCliente clienteTipo, string documento, string? cnh) : this()
    {
        Nome = nome;
        Endereco = endereco;
        Telefone = telefone;
        ClienteTipo = clienteTipo;
        Documento = documento;
        Cnh = cnh;
    }
}
