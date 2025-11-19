using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCondutor;

public class RepositorioCondutorEmOrm(IContextoPersistencia context)
    : RepositorioBase<Condutor>(context), IRepositorioCondutor;