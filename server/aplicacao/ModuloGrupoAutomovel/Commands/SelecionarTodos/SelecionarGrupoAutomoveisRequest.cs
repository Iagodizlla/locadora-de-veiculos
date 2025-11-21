using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.SelecionarTodos;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;

public record SelecionarGrupoAutomoveisRequest : IRequest<Result<SelecionarGrupoAutomoveisResponse>>;