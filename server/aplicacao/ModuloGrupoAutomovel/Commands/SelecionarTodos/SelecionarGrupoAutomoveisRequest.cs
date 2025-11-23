using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.SelecionarTodos;

public record SelecionarGrupoAutomoveisRequest : IRequest<Result<SelecionarGrupoAutomoveisResponse>>;