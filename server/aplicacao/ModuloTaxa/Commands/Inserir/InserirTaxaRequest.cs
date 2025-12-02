using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.Inserir;

public record InserirTaxaRequest(string Nome, double Preco, EServico Servico)
    : IRequest<Result<InserirTaxaResponse>>;
