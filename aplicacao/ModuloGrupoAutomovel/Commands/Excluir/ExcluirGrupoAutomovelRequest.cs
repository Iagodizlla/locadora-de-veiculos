using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Excluir;

public record ExcluirGrupoAutomovelRequest(Guid Id) : IRequest<Result<ExcluirGrupoAutomovelResponse>>;