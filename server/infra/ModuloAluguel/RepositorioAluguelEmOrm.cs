using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloConfig;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAluguel;

public class RepositorioAluguelEmOrm(IContextoPersistencia context, IRepositorioConfig repoConfig)
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

    #region Validacoes

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

    public async Task<bool> AutomovelEstaOcupadoAsync(Guid automovelId)
    {
        return await registros
        .AnyAsync(a =>
            a.Automovel.Id == automovelId &&
            a.Status == false
        );
    }
    #endregion

    public async Task<decimal> CalcularValorTotalDoAluguelAsync(Aluguel aluguel)
    {
        if (!aluguel.DataDevolucao.HasValue || !aluguel.QuilometragemFinal.HasValue)
        {
            return await CalcularValorTotalDoAluguelReservaAsync(aluguel);
        }

        double diasReais = (aluguel.DataDevolucao.Value - aluguel.DataSaida).TotalDays;

        if (diasReais <= 0) diasReais = 1;

        int kmRodada = aluguel.QuilometragemFinal.Value - aluguel.QuilometragemInicial;

        decimal custoRealLocacao = CalcularCustoBaseLocacao(diasReais, kmRodada, aluguel.Plano);

        decimal custoSeguroReal = CalcularCustoSeguro(diasReais, aluguel.ValorSeguroPorDia);

        decimal taxasAdicionais = 0m;

        if (aluguel.NivelCombustivelNaDevolucao < aluguel.NivelCombustivelNaSaida)
        {
            decimal combustivelFaltando = (decimal)(aluguel.NivelCombustivelNaSaida - aluguel.NivelCombustivelNaDevolucao.Value);
            var config = await repoConfig.SelecionarAsync(); 
            var precoCombustivel = 0m;

            if (config != null)
            {
                if (aluguel.Automovel.Combustivel == ECombustivel.Gasolina)
                {
                    precoCombustivel = config.Gasolina * combustivelFaltando;
                }
                else if (aluguel.Automovel.Combustivel == ECombustivel.Gas)
                {
                    precoCombustivel = config.Gas * combustivelFaltando;
                }
                else if (aluguel.Automovel.Combustivel == ECombustivel.Diesel)
                {
                    precoCombustivel = config.Diesel * combustivelFaltando;
                }
                else if (aluguel.Automovel.Combustivel == ECombustivel.Alcool)
                {
                    precoCombustivel = config.Alcool * combustivelFaltando;
                }
            }

            aluguel.ValorTotal += precoCombustivel;
        }

        foreach (var taxa in aluguel.Taxas.Where(t => t.Nome != "Seguro"))
        {
            if (taxa.Servico == EServico.PrecoFixo)
            {
                taxasAdicionais += (decimal)taxa.Preco;
            }
            else if (taxa.Servico == EServico.CobrancaDiaria)
            {
                taxasAdicionais += (decimal)(taxa.Preco * diasReais);
            }
        }

        decimal multaPorAtraso = 0m;
        if (aluguel.DataDevolucao.Value > aluguel.DataRetornoPrevista)
        {
            multaPorAtraso = aluguel.ValorTotal * 0.10m;
        }

        decimal valorTotalFinal = custoRealLocacao + custoSeguroReal + multaPorAtraso + taxasAdicionais;

        return valorTotalFinal;
    }

    public async Task<decimal> CalcularValorTotalDoAluguelReservaAsync(Aluguel aluguel)
    {
        double diasPrevistos = (aluguel.DataRetornoPrevista - aluguel.DataSaida).TotalDays;

        if (diasPrevistos <= 0) diasPrevistos = 1;

        decimal custoBase = CalcularCustoBaseLocacao(diasPrevistos, 0, aluguel.Plano);

        decimal custoSeguro = CalcularCustoSeguro(diasPrevistos, aluguel.ValorSeguroPorDia);

        aluguel.ValorTotal = custoBase + custoSeguro;

        return aluguel.ValorTotal;
    }

    #region metodos privados
    private decimal CalcularCustoBaseLocacao(double dias, int kmReal, Plano plano)
    {
        decimal valor = 0m;

        dias = Math.Ceiling(dias);

        if (plano.PrecoDiario > 0 && plano.PrecoDiarioControlado == 0 && plano.PrecoLivre == 0)
        {
            valor += (decimal)(plano.PrecoDiario * dias);

            if (kmReal > 0)
            {
                valor += (decimal)(kmReal * plano.PrecoPorKm);
            }
        }

        else if (plano.PrecoDiarioControlado > 0)
        {
            valor += (decimal)(plano.PrecoDiarioControlado * dias);

            if (kmReal > 0)
            {
                double kmExtrapolado = Math.Max(0, kmReal - plano.KmLivres);
                valor += (decimal)(kmExtrapolado * plano.PrecoPorKmExplorado);
            }
        }

        else if (plano.PrecoLivre > 0)
        {
            valor += (decimal)(plano.PrecoLivre * dias);
        }

        return valor;
    }
    private decimal CalcularCustoSeguro(double dias, double? valorSeguroPorDia)
    {
        dias = Math.Ceiling(dias);

        if (valorSeguroPorDia.HasValue)
        {
            return (decimal)(valorSeguroPorDia.Value * dias);
        }
        return 0m;
    }
    #endregion
}
