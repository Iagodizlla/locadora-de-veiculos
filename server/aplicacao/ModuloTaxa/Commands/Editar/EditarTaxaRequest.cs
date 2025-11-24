using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Editar;

public record EditarTaxaPartialRequest(string Nome, double Preco, EServico Servico, List<Aluguel> Alugueis);

public record EditarTaxaRequest(Guid Id, string Nome, double Preco, EServico Servico, List<Aluguel> Alugueis)
    : IRequest<Result<EditarTaxaResponse>>;