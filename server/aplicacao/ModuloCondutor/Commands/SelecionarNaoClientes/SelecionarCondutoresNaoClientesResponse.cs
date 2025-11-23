using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarNaoClientes;

public record SelecionarCondutoresNaoClientesDto(
    Guid Id,
    string Nome,
    string Cnh,
    string Cpf,
    string Telefone,
    ECategoria Categoria,
    DateTimeOffset ValidadeCnh,
    bool ECliente
);

public record SelecionarCondutoresNaoClientesResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarCondutoresNaoClientesDto> Registros { get; init; }
}
