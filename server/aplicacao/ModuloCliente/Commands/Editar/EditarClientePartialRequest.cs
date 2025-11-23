using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Inserir;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Editar;

public record EditarClientePartialRequest(string Nome, InserirEnderecoRequest Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh);

public record EditarClienteRequest(Guid Id, string Nome, InserirEnderecoRequest Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh)
    : IRequest<Result<EditarClienteResponse>>;