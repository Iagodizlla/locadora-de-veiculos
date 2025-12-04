using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

namespace LocadoraDeVeiculos.Dominio.ModuloFuncionario;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; }
    public double Salario { get; set; }
    public DateTimeOffset Admissao { get; set; }
    public Usuario Usuario { get; set; }
    public Guid? UsuarioId { get; set; }
    public bool Excluido { get; set; }

    public Funcionario() { }

    public Funcionario(string nome, double salario, DateTimeOffset admissao) : this()
    {
        this.Nome = nome;
        this.Salario = salario;
        this.Admissao = admissao;
    }
}