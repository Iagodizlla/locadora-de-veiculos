using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCondutor;

public class RepositorioCondutorEmOrm(IContextoPersistencia context)
    : RepositorioBase<Condutor>(context), IRepositorioCondutor
{
    public async Task<Condutor?> SelecionarPorCpfAsync(string cpf)
    {
        return await registros
            .Include(a => a.Cliente)
            .FirstOrDefaultAsync(a => a.Cpf == cpf);
    }
    public override async Task<List<Condutor>> SelecionarTodosAsync()
    {
        return await registros
            .Include(a => a.Cliente)
            .ToListAsync();
    }

    public override async Task<Condutor?> SelecionarPorIdAsync(Guid id)
    {
        return await registros
            .Include(a => a.Cliente)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}
