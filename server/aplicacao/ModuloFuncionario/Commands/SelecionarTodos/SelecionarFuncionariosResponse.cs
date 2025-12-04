namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;

public record SelecionarFuncionariosDto(Guid Id, string UserName, double Salario, DateTimeOffset Admissao);

public record SelecionarFuncionariosResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarFuncionariosDto> Registros { get; init; }
}
