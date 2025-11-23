using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Editar;

public record EditarTaxaPartialRequest(string Nome, double Preco, EServico Servico);

public record EditarTaxaRequest(Guid Id, string Nome, double Preco, EServico Servico)
    : IRequest<Result<EditarTaxaResponse>>;