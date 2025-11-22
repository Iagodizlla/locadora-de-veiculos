using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Inserir;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Editar;

public record EditarClientePartialRequest(string Nome, InserirEnderecoRequest Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh, Condutor? Condutor);

public record EditarClienteRequest(Guid Id, string Nome, InserirEnderecoRequest Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh, Condutor? Condutor)
    : IRequest<Result<EditarClienteResponse>>;