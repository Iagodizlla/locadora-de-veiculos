using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloGrupoAutomovel;

public class RepositorioGrupoAutomovelEmOrm(IContextoPersistencia context)
    : RepositorioBase<GrupoAutomovel>(context), IRepositorioGrupoAutomovel;