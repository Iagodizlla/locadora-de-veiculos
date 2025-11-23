using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarTodos;

public record SelecionarTaxasRequest : IRequest<Result<SelecionarTaxasResponse>>;