using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarClientes;

public record SelecionarCondutoresClientesRequest
    : IRequest<Result<SelecionarCondutoresClientesResponse>>;
