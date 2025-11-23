using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCliente;

public class RepositorioClienteEmOrm(IContextoPersistencia context)
    : RepositorioBase<Cliente>(context), IRepositorioCliente
{
    public override async Task<List<Cliente>> SelecionarTodosAsync()
    {
        return await registros
            .Include(a => a.Endereco)
            .ToListAsync();
    }

    public override async Task<Cliente?> SelecionarPorIdAsync(Guid id)
    {
        return await registros
            .Include(a => a.Endereco)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Cliente>> SelecionarClientesPJAsync()
    {
        return await registros
            .Where(c => c.ClienteTipo == ETipoCliente.PessoaJuridica)
            .Include(c => c.Endereco)
            .ToListAsync();
    }

    public async Task<List<Cliente>> SelecionarClientesPFAsync()
    {
        return await registros
            .Where(c => c.ClienteTipo == ETipoCliente.PessoaFisica)
            .Include(c => c.Endereco)
            .ToListAsync();
    }
}