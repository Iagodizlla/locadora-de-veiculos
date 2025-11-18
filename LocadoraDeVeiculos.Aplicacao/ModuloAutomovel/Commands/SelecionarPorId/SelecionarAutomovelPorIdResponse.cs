using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarPorId;

public record SelecionarAutomovelPorIdResponse(Guid Id, string Placa, string Modelo, string Marca, string Cor, int Ano, int CapacidadeTanque, string Foto, GrupoAutomovel GrupoAutomovel, ECombustivel Combustivel);