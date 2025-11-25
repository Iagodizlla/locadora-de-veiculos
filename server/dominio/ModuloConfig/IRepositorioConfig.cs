namespace LocadoraDeVeiculos.Dominio.ModuloConfig;

public interface IRepositorioConfig
{
    Task<Config> SelecionarAsync();
    Task<bool> EditarAsync(Config configuracao);
    Task InserirAsync(Config configuracaoInicial);
}
