using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Editar;

public record EditarClientePartialRequest(string Nome, Endereco Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh);

public record EditarClienteRequest(Guid Id, string Nome, Endereco Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh)
    : IRequest<Result<EditarClienteResponse>>;