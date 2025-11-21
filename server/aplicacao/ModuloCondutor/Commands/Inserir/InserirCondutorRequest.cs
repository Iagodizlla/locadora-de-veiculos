using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Inserir;

public record InserirCondutorRequest(string Nome, string Cnh, ECategoria Categoria, DateTimeOffset ValidadeCnh)
    : IRequest<Result<InserirCondutorResponse>>;
