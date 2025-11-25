using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAluguel;

public class RepositorioAluguelEmOrm(IContextoPersistencia context)
    : RepositorioBase<Aluguel>(context), IRepositorioAluguel
{
    public Task<bool> FinalizarAsync(Aluguel entidadeParaFinalizar)
    {
        entidadeParaFinalizar.Status = true;

        var rastreador = registros.Update(entidadeParaFinalizar);

        return Task.FromResult(rastreador.State == EntityState.Modified);
    }

    public async Task<List<Aluguel>> SelecionarAtivosAsync()
    {
        return await registros
            .Where(a => a.Status == false)
            .Include(a => a.Taxas)
            .Include(a => a.Cliente)
            .Include(a => a.Condutor)
            .Include(a => a.Automovel)
            .Include(a => a.Plano)
            .ToListAsync();
    }

    public async Task<List<Aluguel>> SelecionarFinalizadosAsync()
    {
        return await registros
            .Where(a => a.Status == true)
            .Include(a => a.Taxas)
            .Include(a => a.Cliente)
            .Include(a => a.Condutor)
            .Include(a => a.Automovel)
            .Include(a => a.Plano)
            .ToListAsync();
    }

    public override async Task<List<Aluguel>> SelecionarTodosAsync()
    {
        return await registros
            .Include(a => a.Taxas)
            .Include(a => a.Cliente)
            .Include(a => a.Condutor)
            .Include(a => a.Automovel)
            .Include(a => a.Plano)
            .ToListAsync();
    }

    public override async Task<Aluguel?> SelecionarPorIdAsync(Guid id)
    {
        return await registros
            .Include(a => a.Taxas)
            .Include(a => a.Cliente)
            .Include(a => a.Condutor)
            .Include(a => a.Automovel)
            .Include(a => a.Plano)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> ExisteAluguelComPlanoAsync(Guid planoId)
    {
        return await registros
            .AnyAsync(a => a.PlanoId == planoId);
    }

    public async Task<bool> VeiculoEmAluguelAtivoAsync(Guid automovelId)
    {
        return await registros
            .AnyAsync(a => a.Automovel.Id == automovelId && a.Status == false);
    }

    public async Task<bool> ClienteEmAluguelAtivoAsync(Guid clienteId)
    {
        return await registros
            .AnyAsync(a => a.Cliente.Id == clienteId && a.Status == false);
    }
    public async Task<bool> CondutorEmAluguelAtivoAsync(Guid condutorId)
    {
        return await registros
            .AnyAsync(a => a.Condutor.Id == condutorId && a.Status == false);
    }

    public async Task<bool> TaxaEmAluguelAtivoAsync(Guid taxaId)
    {
        return await registros
        .AnyAsync(a =>
            a.Status == false &&
            a.Taxas.Any(t => t.Id == taxaId)
        );
    }

    public async Task<bool> ClienteEstaOcupadoAsync(Guid clienteId)
    {
        return await registros
        .AnyAsync(a =>
            a.Cliente.Id == clienteId &&
            a.Status == false
        );
    }
    public async Task<bool> CondutorEstaOcupadoAsync(Guid condutorId)
    {
        return await registros
        .AnyAsync(a =>
            a.Condutor.Id == condutorId &&
            a.Status == false
        );
    }
}
