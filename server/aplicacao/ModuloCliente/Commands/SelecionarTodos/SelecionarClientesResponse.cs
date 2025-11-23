using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarTodos;

public record SelecionarClientesDto(Guid Id, string Nome, Endereco Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh);

public record SelecionarClientesResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarClientesDto> Registros { get; init; }
}
