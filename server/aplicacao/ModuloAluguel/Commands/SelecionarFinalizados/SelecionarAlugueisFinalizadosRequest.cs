using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel.Commands.SelecionarFinalizados;

public record SelecionarAlugueisFinalizadosRequest : IRequest<Result<SelecionarAlugueisFinalizadosResponse>>;