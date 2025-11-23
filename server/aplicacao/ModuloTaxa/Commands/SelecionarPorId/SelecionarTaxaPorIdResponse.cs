using LocadoraDeVeiculos.Dominio.ModuloTaxa;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa.Commands.SelecionarPorId;

public record SelecionarTaxaPorIdResponse(Guid Id, string Nome, double Preco, EServico Servico);