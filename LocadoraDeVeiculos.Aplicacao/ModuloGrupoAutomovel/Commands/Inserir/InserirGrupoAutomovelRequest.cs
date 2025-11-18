using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Inserir;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Inserir;

public record InserirGrupoAutomovelRequest(string Nome)
    : IRequest<Result<InserirGrupoAutomovelResponse>>;
