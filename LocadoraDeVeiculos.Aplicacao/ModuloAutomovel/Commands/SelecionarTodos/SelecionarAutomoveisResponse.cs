using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarTodos;

public record SelecionarAutomoveisDto(Guid Id, string Placa, string Modelo, string Marca, string Cor, int Ano, int CapacidadeTanque, string Foto, GrupoAutomovel GrupoAutomovel, ECombustivel Combustivel);

public record SelecionarAutomoveisResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarAutomoveisDto> Registros { get; init; }
}
