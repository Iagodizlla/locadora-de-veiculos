using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Excluir;

public record ExcluirPlanoRequest(Guid Id) : IRequest<Result<ExcluirPlanoResponse>>;