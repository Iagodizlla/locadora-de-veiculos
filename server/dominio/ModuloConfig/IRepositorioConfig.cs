namespace LocadoraDeVeiculos.Dominio.ModuloConfig;

public interface IRepositorioConfig
{
    Task<bool> EditarAsync(Config entidadeAtualizada);
    Task<Config?> SelecionarPorIdAsync(Guid id);
}
