using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Editar;

public record EditarAluguelPartialRequest(Cliente Cliente, Condutor Condutor, Automovel Automovel, Plano Plano, List<Taxa> Taxas, DateTimeOffset DataSaisa, DateTimeOffset DataRetornoPrevista,
    DateTimeOffset? DataDevolucao, int KmInicial, int?KmFinal, int NivelCombustivelNaSaida, int? NivelCombustivelNaDevolucao, bool SeguroCliente, bool SeguroTerceiro, double? ValorSeguroPorDia, bool Status);

public record EditarAluguelRequest(Guid Id, Cliente Cliente, Condutor Condutor, Automovel Automovel, Plano Plano, List<Taxa> Taxas, DateTimeOffset DataSaisa, DateTimeOffset DataRetornoPrevista,
    DateTimeOffset? DataDevolucao, int KmInicial, int? KmFianl, int NivelCombustivelNaSaida, int? NivelCombustivelNaDevolucao, bool SeguroCliente, bool SeguroTerceiro, double? ValorSeguroPorDia, bool Status)
    : IRequest<Result<EditarAluguelResponse>>;