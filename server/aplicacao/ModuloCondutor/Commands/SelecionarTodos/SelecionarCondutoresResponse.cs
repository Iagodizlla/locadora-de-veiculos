using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarTodos;

public record SelecionarCondutoresDto(Guid Id, string Nome, string Cnh, string Cpf, string Telefone, ECategoria Categoria, DateTimeOffset ValidadeCnh, Cliente Cliente, bool ECliente);

public record SelecionarCondutoresResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarCondutoresDto> Registros { get; init; }
}
