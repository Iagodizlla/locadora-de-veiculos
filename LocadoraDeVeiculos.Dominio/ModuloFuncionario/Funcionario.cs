using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloFuncionario;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; }
    public double Salario { get; set; }
    public DateTimeOffset Admissao { get; set; }

    public Funcionario(string nome, double salario, DateTimeOffset admissao)
    {
        this.Nome = nome;
        this.Salario = salario;
        this.Admissao = admissao;
    }
}