using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarPorId;

public record SelecionarCondutorPorIdResponse(Guid Id, string Nome, string Cnh, string Cpf, string Telefone, ECategoria Categoria, DateTimeOffset ValidadeCnh, Cliente Cliente, bool ECliente);