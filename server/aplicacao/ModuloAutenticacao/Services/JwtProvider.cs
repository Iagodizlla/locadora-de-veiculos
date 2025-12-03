using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.DTOs;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.Services;

public class JwtProvider : ITokenProvider
{
    private readonly string? chaveJwt;
    private readonly DateTime dataExpiracaoJwt;
    private string? audienciaValida;
    private readonly UserManager<Usuario> userManager;

    public JwtProvider(IConfiguration config, UserManager<Usuario> userManager)
    {
        chaveJwt = config["JWT_GENERATION_KEY"];

        if (string.IsNullOrEmpty(chaveJwt))
            throw new ArgumentException("Chave de geração de tokens não configurada");

        audienciaValida = config["JWT_AUDIENCE_DOMAIN"];

        if (string.IsNullOrEmpty(audienciaValida))
            throw new ArgumentException("Audiência válida para transmissão de tokens não configurada");

        dataExpiracaoJwt = DateTime.UtcNow.AddMinutes(30);

        this.userManager = userManager;
    }

    public async Task<IAccessToken> GerarTokenDeAcessoAsync(Usuario usuario)
    {
        var roles = await userManager.GetRolesAsync(usuario);

        var cargoDoUsuarioStr = roles.FirstOrDefault(); // Empresa / Funcionario

        if (cargoDoUsuarioStr is null)
            throw new Exception("Não foi possível recuperar os dados de permissão do usuário.");

        var tokenHandler = new JwtSecurityTokenHandler();

        var chaveEmBytes = Encoding.ASCII.GetBytes(chaveJwt!);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email!),
            new Claim("usuario_id", usuario.Id.ToString()),
            new Claim("EmpresaId", usuario.EmpresaId.ToString()),
            new Claim(ClaimTypes.Role, cargoDoUsuarioStr),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "LocadoraVeiculo",
            Audience = audienciaValida,
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(chaveEmBytes),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Expires = dataExpiracaoJwt,
            NotBefore = DateTime.UtcNow
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var tokenString = tokenHandler.WriteToken(token);

        return new TokenResponse()
        {
            Chave = tokenString,
            DataExpiracao = dataExpiracaoJwt,
            Usuario = new UsuarioAutenticadoDto
            {
                Id = usuario.Id,
                UserName = usuario.UserName!,
                Email = usuario.Email!
            }
        };
    }
}