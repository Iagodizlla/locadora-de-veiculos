namespace LocadoraDeVeiculos.Dominio.ModuloCondutor;

public interface IRepositorioCondutor
{
    Task<Guid> InserirAsync(Condutor novaEntidade);
    Task<bool> EditarAsync(Condutor entidadeAtualizada);
    Task<bool> ExcluirAsync(Condutor entidadeParaRemover);
    Task<List<Condutor>> SelecionarTodosAsync();
    Task<Condutor?> SelecionarPorIdAsync(Guid id);
}