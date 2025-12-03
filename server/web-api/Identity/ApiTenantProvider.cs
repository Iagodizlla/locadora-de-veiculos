using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LocadoraDeVeiculos.WebApi.Identity;

public class ApiTenantProvider(IHttpContextAccessor contextAccessor) : ITenantProvider, IContextoUsuario
{
    public Guid? EmpresaId
    {
        get
        {
            var claimId = contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

            var empresaIdClaim = user?.FindFirst("EmpresaId")?.Value;

            return Guid.Parse(claimId.Value);
        }
    }
    public Guid? UsuarioId
    {
        get
        {
            ClaimsPrincipal? claimsPrincipal = contextAccessor.HttpContext?.User;

            if (claimsPrincipal?.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            Claim? claimId = claimsPrincipal.FindFirst("usuario_id");

            if (claimId == null)
            {
                return null;
            }

            return Guid.Parse(claimId.Value);
        }
    }

    public bool IsInRole(string nome) => contextAccessor.HttpContext?.User?.IsInRole(nome) ?? false;
}