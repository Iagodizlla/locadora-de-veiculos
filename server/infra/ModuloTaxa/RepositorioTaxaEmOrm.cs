using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloTaxa;

public class RepositorioTaxaEmOrm(IContextoPersistencia context)
    : RepositorioBase<Taxa>(context), IRepositorioTaxa
{
    public async Task<List<Taxa>> SelecionarTodosPorIdAsync(List<Guid> entidadesId)
    {
        if (entidadesId == null || entidadesId.Count == 0)
            return new List<Taxa>();

        return await registros
            .Where(t => entidadesId.Contains(t.Id))
            .ToListAsync();
    }
}
