using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Editar;

public record EditarGrupoAutomovelPartialRequest(string Nome);

public record EditarGrupoAutomovelRequest(Guid Id, string Nome)
    : IRequest<Result<EditarGrupoAutomovelResponse>>;