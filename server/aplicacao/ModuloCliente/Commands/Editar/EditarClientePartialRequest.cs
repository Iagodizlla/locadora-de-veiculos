using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Inserir;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Editar;

public record EditarClientePartialRequest(string Nome, EditarEnderecoRequest Endereco, string Telefone, ETipoCliente ClienteTipo, string Documento, string? Cnh);

public record EditarClienteRequest(Guid Id, string Nome, EditarEnderecoRequest Endereco, string Telefone, ETipoCliente ClienteTipo, string Documento, string? Cnh)
    : IRequest<Result<EditarClienteResponse>>;

public record EditarEnderecoRequest(
    string Logradouro,
    int Numero,
    string Bairro,
    string Cidade,
    string Estado
);
