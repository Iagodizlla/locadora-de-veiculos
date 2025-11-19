using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

namespace LocadoraDeVeiculos.Dominio.Compartilhado;

public abstract class EntidadeBase
{
    public Guid Id { get; set; }

    protected EntidadeBase()
    {
        Id = Guid.NewGuid();
    }

    public Guid EmpresaId { get; set; }
    public Usuario? Empresa { get; set; }
}