using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarAtivos;

public record SelecionarAlugueisAtivosRequest : IRequest<Result<SelecionarAlugueisAtivosResponse>>;