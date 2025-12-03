using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloFuncionario;

public class RepositorioFuncionarioEmOrm(IContextoPersistencia context)
    : RepositorioBase<Funcionario>(context), IRepositorioFuncionario
{
    public async Task<Funcionario> SelecionarPorUsuarioIdAsync(Guid usuarioId)
    {
        return await registros
            .FirstOrDefaultAsync(f => f.UsuarioId == usuarioId);
    }
}