namespace LocadoraDeVeiculos.Dominio.ModuloCliente;

public interface IRepositorioCliente
{
    Task<Guid> InserirAsync(Cliente novaEntidade);
    Task<bool> EditarAsync(Cliente entidadeAtualizada);
    Task<bool> ExcluirAsync(Cliente entidadeParaRemover);
    Task<List<Cliente>> SelecionarTodosAsync();
    Task<Cliente?> SelecionarPorIdAsync(Guid id);
    Task<List<Cliente>> SelecionarClientesPJAsync();
    Task<List<Cliente>> SelecionarClientesPFAsync();
}
