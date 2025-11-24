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
            .FirstOrDefaultAsync(a => a.Cpf == cpf);
    }
    public async Task<List<Condutor>> SelecionarNaoClientesAsync()
    {
        return await registros
            .Where(c => c.ECliente == false)
            .ToListAsync();
    }

    public async Task<List<Condutor>> SelecionarClientesAsync()
    {
        return await registros
            .Where(c => c.ECliente == true)
            .ToListAsync();
    }
}
