using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

public class Usuario : IdentityUser<Guid>
{
    public Usuario()
    {
        Id = Guid.NewGuid();
        EmailConfirmed = true;
    }
}