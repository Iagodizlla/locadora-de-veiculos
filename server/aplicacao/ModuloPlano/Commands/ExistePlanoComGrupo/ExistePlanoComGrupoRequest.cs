using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.ExistePlanoComGrupo;

public record ExistePlanoComGrupoRequest(Guid GrupoAutomovelId)
    : IRequest<Result<ExistePlanoComGrupoResponse>>;
