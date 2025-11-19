using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarPorId;

public record SelecionarCondutorPorIdResponse(Guid Id, string Nome, string Cnh, ECategoria Categoria, DateTimeOffset ValidadeCnh);