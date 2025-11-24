namespace LocadoraDeVeiculos.Dominio.ModuloTaxa;

public interface IRepositorioTaxa
{
    Task<Guid> InserirAsync(Taxa novaEntidade);
    Task<bool> EditarAsync(Taxa entidadeAtualizada);
    Task<bool> ExcluirAsync(Taxa entidadeParaRemover);
    Task<List<Taxa>> SelecionarTodosAsync();
    Task<List<Taxa>> SelecionarTodosPorIdAsync(List<Guid> entidadesId);
    Task<Taxa?> SelecionarPorIdAsync(Guid id);
}