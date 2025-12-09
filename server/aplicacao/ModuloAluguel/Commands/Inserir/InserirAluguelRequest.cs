using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.Inserir;

public record InserirAluguelRequest(Guid ClienteId, Guid CondutorId, Guid AutomovelId, Guid PlanoId, IEnumerable<Guid> Taxas, DateTimeOffset DataSaida, DateTimeOffset DataRetornoPrevista,
    int KmInicial, int NivelCombustivelNaSaida, bool SeguroCliente, bool SeguroTerceiro, double? ValorSeguroPorDia)
    : IRequest<Result<InserirAluguelResponse>>;
