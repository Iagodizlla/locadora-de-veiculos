using LocadoraDeVeiculos.Dominio.ModuloAutomovel;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarPorGrupo;

public record SelecionarAutomoveisPorGrupoDto(
    Guid Id,
    string Placa,
    string Modelo,
    string Marca,
    string Cor,
    int Ano,
    int CapacidadeTanque,
    string Foto,
    ECombustivel Combustivel
);

public record SelecionarAutomoveisPorGrupoResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarAutomoveisPorGrupoDto> Registros { get; init; }
}
