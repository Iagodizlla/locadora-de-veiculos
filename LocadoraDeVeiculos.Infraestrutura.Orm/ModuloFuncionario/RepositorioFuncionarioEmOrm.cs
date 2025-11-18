using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloFuncionario;

public class RepositorioFuncionarioEmOrm(IContextoPersistencia context) 
    : RepositorioBase<Funcionario>(context), IRepositorioFuncionario;