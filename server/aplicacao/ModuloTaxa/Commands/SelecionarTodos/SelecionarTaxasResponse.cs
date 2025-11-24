using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarTodos;

public record SelecionarTaxasDto(Guid Id, string Nome, double Preco, EServico Servico, List<Aluguel> Alugueis);

public record SelecionarTaxasResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarTaxasDto> Registros { get; init; }
}
