using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Excluir;

public record ExcluirCondutorRequest(Guid Id) : IRequest<Result<ExcluirCondutorResponse>>;