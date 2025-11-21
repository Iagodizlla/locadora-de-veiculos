using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.SelecionarPorId;

public record SelecionarPlanoPorIdResponse(Guid Id, ETipoPlano TipoPlano, GrupoAutomovel GrupoAutomovel, double? PrecoDiario, double? PrecoPorKm, double? KmLivres, double? PrecoporKmExplorado, double? PrecoLivre);