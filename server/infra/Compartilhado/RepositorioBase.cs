using LocadoraDeVeiculos.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;

public class RepositorioBase<TEntidade> where TEntidade : EntidadeBase
{
    protected readonly IContextoPersistencia context;
    protected readonly DbSet<TEntidade> registros;

    public RepositorioBase(IContextoPersistencia context)
    {
        this.context = context;
        registros = ((DbContext)this.context).Set<TEntidade>();
    }

    public async Task<Guid> InserirAsync(TEntidade registro)
    {
        await registros.AddAsync(registro);

        return registro.Id;
    }

    public async Task<bool> EditarAsync(TEntidade registro)
    {
        var rastreador = registros.Update(registro);

        return await Task.Run(() => rastreador.State == EntityState.Modified);
    }

    public async Task<bool> ExcluirAsync(TEntidade registro)
    {
        var rastreador = registros.Remove(registro);

        return await Task.Run(() => rastreador.State == EntityState.Deleted);
    }

    public virtual async Task<List<TEntidade>> SelecionarTodosAsync()
    {
        return await registros.ToListAsync();
    }

    public virtual async Task<TEntidade?> SelecionarPorIdAsync(Guid id)
    {
        return await registros.SingleOrDefaultAsync(x => x.Id == id);
    }
}