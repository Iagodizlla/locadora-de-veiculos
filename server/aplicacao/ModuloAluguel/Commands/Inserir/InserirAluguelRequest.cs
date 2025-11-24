using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Inserir;

public record InserirAluguelRequest(Guid ClienteId, Guid CondutorId, Guid AutomovelId, Guid PlanoId, List<Guid> TaxasId, DateTimeOffset DataSaisa, DateTimeOffset DataRetornoPrevista,
    DateTimeOffset? DataDevolucao, int KmInicial, int? KmFianl, int NivelCombustivelNaSaida, int? NivelCombustivelNaDevolucao, bool SeguroCliente, bool SeguroTerceiro, double? ValorSeguroPorDia, bool Status)
    : IRequest<Result<InserirAluguelResponse>>;
