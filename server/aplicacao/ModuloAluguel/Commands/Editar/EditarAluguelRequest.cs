using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Editar;

public record EditarAluguelPartialRequest(Guid ClienteId, Guid CondutorId, Guid AutomovelId, Guid PlanoId, List<Guid> TaxasId, DateTimeOffset DataSaida, DateTimeOffset DataRetornoPrevista,
    DateTimeOffset? DataDevolucao, int KmInicial, int?KmFinal, int NivelCombustivelNaSaida, int? NivelCombustivelNaDevolucao, bool SeguroCliente, bool SeguroTerceiro, double? ValorSeguroPorDia);

public record EditarAluguelRequest(Guid Id, Guid ClienteId, Guid CondutorId, Guid AutomovelId, Guid PlanoId, List<Guid> TaxasId, DateTimeOffset DataSaida, DateTimeOffset DataRetornoPrevista,
    DateTimeOffset? DataDevolucao, int KmInicial, int? KmFianl, int NivelCombustivelNaSaida, int? NivelCombustivelNaDevolucao, bool SeguroCliente, bool SeguroTerceiro, double? ValorSeguroPorDia)
    : IRequest<Result<EditarAluguelResponse>>;