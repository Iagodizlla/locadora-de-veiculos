using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

namespace LocadoraDeVeiculos.Dominio.ModuloFuncionario;

public class Funcionario : EntidadeBase
{
    public double Salario { get; set; }
    public DateTimeOffset Admissao { get; set; }
    public Usuario Usuario { get; set; }
    public Guid? UsuarioId { get; set; }
    public bool Excluido { get; set; }

    public Funcionario() { }

    public Funcionario(string userName, double salario, DateTimeOffset admissao) : this()
    {
        this.Usuario.UserName = userName;
        this.Salario = salario;
        this.Admissao = admissao;
    }
}