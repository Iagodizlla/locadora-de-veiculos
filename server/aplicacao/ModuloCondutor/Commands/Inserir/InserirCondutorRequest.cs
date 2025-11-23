using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Inserir;

public record InserirCondutorRequest(string Nome, string Cnh, string Cpf, string Telefone, ECategoria Categoria, DateTimeOffset ValidadeCnh, bool ECliente)
    : IRequest<Result<InserirCondutorResponse>>;
