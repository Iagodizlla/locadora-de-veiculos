using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarPorId;

public record SelecionarCondutorPorIdRequest(Guid Id) : IRequest<Result<SelecionarCondutorPorIdResponse>>;