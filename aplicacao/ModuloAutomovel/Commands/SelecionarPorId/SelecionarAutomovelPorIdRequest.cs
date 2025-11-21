using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarPorId;

public record SelecionarAutomovelPorIdRequest(Guid Id) : IRequest<Result<SelecionarAutomovelPorIdResponse>>;