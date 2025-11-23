using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarNaoClientes;

public record SelecionarCondutoresNaoClientesRequest
    : IRequest<Result<SelecionarCondutoresNaoClientesResponse>>;
