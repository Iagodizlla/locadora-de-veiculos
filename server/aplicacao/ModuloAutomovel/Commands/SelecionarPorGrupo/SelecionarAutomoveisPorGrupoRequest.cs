using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel.Commands.SelecionarPorGrupo;

public record SelecionarAutomoveisPorGrupoRequest(Guid GrupoId)
    : IRequest<Result<SelecionarAutomoveisPorGrupoResponse>>;
