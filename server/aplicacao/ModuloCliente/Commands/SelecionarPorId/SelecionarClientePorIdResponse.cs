using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarPorId;

public record SelecionarClientePorIdResponse(Guid Id, string Nome, Endereco Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh, Condutor? Condutor);