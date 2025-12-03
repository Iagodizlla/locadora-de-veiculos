using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LocadoraDeVeiculos.WebApi.Identity;

public class ApiTenantProvider(IHttpContextAccessor contextAccessor) : ITenantProvider
{
    public Guid? EmpresaId
    {
        get
        {
            var user = contextAccessor.HttpContext?.User;

            var empresaIdClaim = user?.FindFirst("EmpresaId")?.Value;

            if (Guid.TryParse(empresaIdClaim, out var empresaId))
                return empresaId;

            return null;
        }
    }
    public bool EstaNoCargo(string cargo)
    {
        return contextAccessor.HttpContext?.User?.IsInRole(cargo) ?? false;
    }
}