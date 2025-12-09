using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarTodos;

public record SelecionarAlugueisDto(Guid Id, Cliente Cliente, Condutor Condutor, Automovel Automovel, Plano Plano, List<Taxa> Taxas, DateTimeOffset DataSaida, DateTimeOffset DataRetornoPrevista,
    DateTimeOffset? DataDevolucao, int KmInicial, int? KmFinal, int NivelCombustivelNaSaida, int? NivelCombustivelNaDevolucao, bool SeguroCliente, bool SeguroTerceiro, double? ValorSeguroPorDia, bool Status, decimal valorTotal);

public record SelecionarAlugueisResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarAlugueisDto> Registros { get; init; }
}
