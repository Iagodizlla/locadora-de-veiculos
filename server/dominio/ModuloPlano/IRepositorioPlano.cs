namespace LocadoraDeVeiculos.Dominio.ModuloPlano;

public interface IRepositorioPlano
{
    Task<Guid> InserirAsync(Plano novaEntidade);
    Task<bool> EditarAsync(Plano entidadeAtualizada);
    Task<bool> ExcluirAsync(Plano entidadeParaRemover);
    Task<List<Plano>> SelecionarTodosAsync();
    Task<Plano?> SelecionarPorIdAsync(Guid id);
}
