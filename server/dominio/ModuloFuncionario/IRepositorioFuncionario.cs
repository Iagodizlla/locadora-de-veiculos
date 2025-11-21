namespace LocadoraDeVeiculos.Dominio.ModuloFuncionario;

public interface IRepositorioFuncionario
{
    Task<Guid> InserirAsync(Funcionario novaEntidade);
    Task<bool> EditarAsync(Funcionario entidadeAtualizada);
    Task<bool> ExcluirAsync(Funcionario entidadeParaRemover);
    Task<List<Funcionario>> SelecionarTodosAsync();
    Task<Funcionario?> SelecionarPorIdAsync(Guid id);
}
