using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarPorId;

public record SelecionarAluguelPorIdResponse(Guid Id, Cliente Cliente, Condutor Condutor, Automovel Automovel, Plano Plano, List<Taxa> Taxas, DateTimeOffset DataSaisa, DateTimeOffset DataRetornoPrevista,
    DateTimeOffset? DataDevolucao, int KmInicial, int? KmFinal, int NivelCombustivelNaSaida, int? NivelCombustivelNaDevolucao, bool SeguroCliente, bool SeguroTerceiro, double? ValorSeguroPorDia, bool Status);