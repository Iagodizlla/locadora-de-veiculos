using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloPlano;

public class RepositorioPlanoEmOrm(IContextoPersistencia context)
    : RepositorioBase<Plano>(context), IRepositorioPlano
{
    public override async Task<List<Plano>> SelecionarTodosAsync()
    {
        return await registros
            .Include(a => a.GrupoAutomovel)
            .ToListAsync();
    }

    public override async Task<Plano?> SelecionarPorIdAsync(Guid id)
    {
        return await registros
            .Include(a => a.GrupoAutomovel)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}