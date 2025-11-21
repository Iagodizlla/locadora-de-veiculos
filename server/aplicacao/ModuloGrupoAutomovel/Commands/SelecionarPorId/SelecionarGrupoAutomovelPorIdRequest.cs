using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.SelecionarPorId;

public record SelecionarGrupoAutomovelPorIdRequest(Guid Id) : IRequest<Result<SelecionarGrupoAutomovelPorIdResponse>>;