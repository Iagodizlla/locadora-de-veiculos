namespace LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

public interface ITokenProvider
{
    Task<IAccessToken> GerarTokenDeAcessoAsync(Usuario usuario);
}