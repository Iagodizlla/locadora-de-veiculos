namespace LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

public interface ITenantProvider
{
    Guid? EmpresaId { get; }
    bool EstaNoCargo(string cargo);
}
