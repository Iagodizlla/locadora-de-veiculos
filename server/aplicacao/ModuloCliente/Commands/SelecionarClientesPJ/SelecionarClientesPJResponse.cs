using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarClientesPJ;

public record SelecionarClientesPJDto(
    Guid Id,
    string Nome,
    Endereco Endereco,
    string Telefone,
    string Documento,
    string? Cnh,
    Condutor? Condutor
);
public record SelecionarClientesPJResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarClientesPJDto> Registros { get; init; }
}