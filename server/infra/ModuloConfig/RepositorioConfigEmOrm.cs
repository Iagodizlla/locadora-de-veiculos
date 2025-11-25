using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloConfig;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloConfig;

public class RepositorioConfigEmOrm(IContextoPersistencia context)
    : RepositorioBase<Config>(context), IRepositorioConfig
{
    public async Task<Config> SelecionarAsync()
    {
        return await registros.FirstOrDefaultAsync();
    }

    Task IRepositorioConfig.InserirAsync(Config configuracaoInicial)
    {
        return InserirAsync(configuracaoInicial);
    }
}
