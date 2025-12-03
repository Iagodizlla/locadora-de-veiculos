using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using System.Security.Claims;

namespace LocadoraDeVeiculos.WebApi.Identity;

public class ContextoUsuario(IHttpContextAccessor accessor) : IContextoUsuario
{
    public Guid? UsuarioId
    {
        get
        {
            var claim = accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            if (Guid.TryParse(claim.Value, out var id))
                return id;

            return null;
        }
    }

    public bool IsInRole(string nome)
    {
        return accessor.HttpContext?.User?.IsInRole(nome) ?? false;
    }
}