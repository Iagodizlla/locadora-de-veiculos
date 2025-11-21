using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.Inserir;

public record InserirAutomovelRequest(string Placa, string Modelo, string Marca, string Cor, int Ano, int CapacidadeTanque, string Foto, Guid GrupoAutomovelId, ECombustivel Combustivel)
    : IRequest<Result<InserirAutomovelResponse>>;
