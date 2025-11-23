using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloTaxa;

public class RepositorioTaxaEmOrm(IContextoPersistencia context)
    : RepositorioBase<Taxa>(context), IRepositorioTaxa;