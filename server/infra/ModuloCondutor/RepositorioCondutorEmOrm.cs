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
}
