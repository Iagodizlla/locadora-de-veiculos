using LocadoraDeVeiculos.Dominio.ModuloCliente;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarClientesPJ;

public record SelecionarClientesPJDto(
    Guid Id,
    string Nome,
    Endereco Endereco,
    string Telefone,
    string Documento,
    string? Cnh
);
public record SelecionarClientesPJResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarClientesPJDto> Registros { get; init; }
}