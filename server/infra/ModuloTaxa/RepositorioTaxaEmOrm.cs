using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloTaxa;

public class RepositorioTaxaEmOrm(IContextoPersistencia context)
    : RepositorioBase<Taxa>(context), IRepositorioTaxa
{
    public async Task<List<Taxa>> SelecionarTodosPorIdAsync(IEnumerable<Guid> entidadesId)
    {
        return await registros
            .Where(t => entidadesId.Contains(t.Id))
            .ToListAsync();
    }
}
