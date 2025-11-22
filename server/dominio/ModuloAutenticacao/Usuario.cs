using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

public class Usuario : IdentityUser<Guid>
{
    public Guid EmpresaId { get; set; }
    public Usuario()
    {
        Id = Guid.NewGuid();
        EmailConfirmed = true;
    }
    public void AssociarEmpresa(Guid tenantId) => this.EmpresaId = tenantId;
}