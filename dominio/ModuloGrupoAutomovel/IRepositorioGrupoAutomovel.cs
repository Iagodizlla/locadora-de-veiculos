namespace LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

public interface IRepositorioGrupoAutomovel
{
    Task<Guid> InserirAsync(GrupoAutomovel novaEntidade);
    Task<bool> EditarAsync(GrupoAutomovel entidadeAtualizada);
    Task<bool> ExcluirAsync(GrupoAutomovel entidadeParaRemover);
    Task<List<GrupoAutomovel>> SelecionarTodosAsync();
    Task<GrupoAutomovel?> SelecionarPorIdAsync(Guid id);
}