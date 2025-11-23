using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarClientes;

public record SelecionarCondutoresClientesDto(
    Guid Id,
    string Nome,
    string Cnh,
    string Cpf,
    string Telefone,
    ECategoria Categoria,
    DateTimeOffset ValidadeCnh,
    bool ECliente
);

public record SelecionarCondutoresClientesResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarCondutoresClientesDto> Registros { get; init; }
}
