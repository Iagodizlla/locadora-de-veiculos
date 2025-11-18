namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.SelecionarTodos;

public record SelecionarGrupoAutomoveisDto(Guid Id, string Nome);

public record SelecionarGrupoAutomoveisResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarGrupoAutomoveisDto> Registros { get; init; }
}
