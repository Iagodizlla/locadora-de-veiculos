using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.Editar;

public record EditarCondutorPartialRequest(string Nome, string Cnh, string Cpf, string Telefone, ECategoria Categoria, DateTimeOffset ValidadeCnh, Cliente? Cliente, bool ECliente);

public record EditarCondutorRequest(Guid Id, string Nome, string Cnh, string Cpf, string Telefone, ECategoria Categoria, DateTimeOffset ValidadeCnh, Cliente? Cliente, bool ECliente)
    : IRequest<Result<EditarCondutorResponse>>;