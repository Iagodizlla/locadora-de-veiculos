using LocadoraDeVeiculos.Dominio.ModuloCliente;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarClientesPF;

public record SelecionarClientesPFDto(
    Guid Id,
    string Nome,
    Endereco Endereco,
    string Telefone,
    string Documento,
    string? Cnh
);
public record SelecionarClientesPFResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarClientesPFDto> Registros { get; init; }
}