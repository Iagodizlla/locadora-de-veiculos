using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Inserir;

public record InserirClienteRequest(string Nome, InserirEnderecoRequest Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh)
    : IRequest<Result<InserirClienteResponse>>;

public record InserirEnderecoRequest(string Logradouro, int Numero, string Bairro, string Cidade, string Estado);