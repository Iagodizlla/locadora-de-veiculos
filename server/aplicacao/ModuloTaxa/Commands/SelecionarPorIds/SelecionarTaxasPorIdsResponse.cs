using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarPorIds;

public record SelecionarTaxasPorIdsDto(Guid Id, string Nome, double Preco, EServico Servico);

public record SelecionarTaxasPorIdsResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarTaxasPorIdsDto> Registros { get; init; }
}
