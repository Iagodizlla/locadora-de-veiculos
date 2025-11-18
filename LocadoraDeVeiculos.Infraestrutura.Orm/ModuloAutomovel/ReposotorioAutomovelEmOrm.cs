using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAutomovel;

public class RepositorioAutomovelEmOrm(IContextoPersistencia context)
    : RepositorioBase<Automovel>(context), IRepositorioAutomovel
{
    public override async Task<List<Automovel>> SelecionarTodosAsync()
    {
        return await registros
            .Include(a => a.GrupoAutomovel)
            .ToListAsync();
    }

    public override async Task<Automovel?> SelecionarPorIdAsync(Guid id)
    {
        return await registros
            .Include(a => a.GrupoAutomovel)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}