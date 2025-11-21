using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente.Commands.Inserir;

public record InserirClienteRequest(string Nome, Endereco Endereco, string Telefone, ETipoCliente TipoCliente, string Documento, string? Cnh)
    : IRequest<Result<InserirClienteResponse>>;
