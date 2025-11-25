namespace LocadoraDeVeiculos.Dominio.ModuloAluguel;

public interface IRepositorioAluguel
{
    Task<Guid> InserirAsync(Aluguel novaEntidade);
    Task<bool> EditarAsync(Aluguel entidadeAtualizada);
    Task<bool> ExcluirAsync(Aluguel entidadeParaRemover);
    Task<List<Aluguel>> SelecionarTodosAsync();
    Task<Aluguel?> SelecionarPorIdAsync(Guid id);
    Task<bool> FinalizarAsync(Aluguel entidadeParaFinalizar);
    Task<List<Aluguel>> SelecionarAtivosAsync();
    Task<List<Aluguel>> SelecionarFinalizadosAsync();
    Task<bool> ExisteAluguelComPlanoAsync(Guid planoId);
    Task<bool> VeiculoEmAluguelAtivoAsync(Guid veiculoId);
    Task<bool> ClienteEmAluguelAtivoAsync(Guid clienteId);
    Task<bool> CondutorEmAluguelAtivoAsync(Guid condutorId);
    Task<bool> TaxaEmAluguelAtivoAsync(Guid taxaId);
}
