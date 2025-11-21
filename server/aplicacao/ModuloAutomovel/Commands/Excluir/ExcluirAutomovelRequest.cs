using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.Excluir;

public record ExcluirAutomovelRequest(Guid Id) : IRequest<Result<ExcluirAutomovelResponse>>;