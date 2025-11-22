using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.SelecionarPorId;

public record SelecionarPlanoPorIdResponse(Guid Id, GrupoAutomovel GrupoAutomovel, double PrecoDiario, double PrecoDiarioControlado, double PrecoPorKm, double KmLivres, double PrecoporKmExplorado, double PrecoLivre);