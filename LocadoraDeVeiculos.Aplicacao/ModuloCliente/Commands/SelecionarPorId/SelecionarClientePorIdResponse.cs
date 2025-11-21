using LocadoraDeVeiculos.Dominio.ModuloCliente;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.SelecionarPorId;

public record SelecionarClientePorIdResponse(Guid Id, string Nome, Endereco Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh);