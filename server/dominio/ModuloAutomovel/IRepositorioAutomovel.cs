namespace LocadoraDeVeiculos.Dominio.ModuloAutomovel;

public interface IRepositorioAutomovel
{
    Task<Guid> InserirAsync(Automovel novaEntidade);
    Task<bool> EditarAsync(Automovel entidadeAtualizada);
    Task<bool> ExcluirAsync(Automovel entidadeParaRemover);
    Task<List<Automovel>> SelecionarTodosAsync();
    Task<Automovel?> SelecionarPorIdAsync(Guid id);
    Task<bool> ExisteAutomovelComGrupoAsync(Guid grupoAutomovelId);
    Task<List<Automovel>> SelecionarPorGrupoAsync(Guid grupoAutomovelId);
}