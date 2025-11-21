using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.SelecionarTodos;

public record SelecionarPlanosDto(Guid Id, ETipoPlano TipoPlano, GrupoAutomovel GrupoAutomovel, double? PrecoDiario, double? PrecoPorKm, double? KmLivres, double? PrecoporKmExplorado, double? PrecoLivre);

public record SelecionarPlanosResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarPlanosDto> Registros { get; init; }
}
