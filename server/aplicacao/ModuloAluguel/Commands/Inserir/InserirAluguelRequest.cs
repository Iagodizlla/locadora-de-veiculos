using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Inserir;

public record InserirAluguelRequest(Guid ClienteId, Guid CondutorId, Guid AutomovelId, Guid PlanoId, List<Guid> TaxasId, DateTimeOffset DataSaisa, DateTimeOffset DataRetornoPrevista,
    DateTimeOffset? DataDevolucao, int KmInicial, int? KmFinal, int NivelCombustivelNaSaida, int? NivelCombustivelNaDevolucao, bool SeguroCliente, bool SeguroTerceiro, double? ValorSeguroPorDia, bool Status, decimal valorTotal)
    : IRequest<Result<InserirAluguelResponse>>;
