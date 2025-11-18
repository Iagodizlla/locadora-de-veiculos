using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.Editar;

public record EditarAutomovelPartialRequest(string Placa, string Modelo, string Marca, string Cor, int Ano, int CapacidadeTanque, string Foto, GrupoAutomovel GrupoAutomovel, ECombustivel Combustivel);

public record EditarAutomovelRequest(Guid Id, string Placa, string Modelo, string Marca, string Cor, int Ano, int CapacidadeTanque, string Foto, GrupoAutomovel GrupoAutomovel, ECombustivel Combustivel)
    : IRequest<Result<EditarAutomovelResponse>>;