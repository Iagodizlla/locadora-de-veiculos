using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Editar;

public record EditarPlanoPartialRequest(Guid GrupoAutomovelId, double PrecoDiario, double PrecoDiarioControlado, double PrecoPorKm, double KmLivres, double PrecoporKmExplorado, double PrecoLivre);

public record EditarPlanoRequest(Guid Id, Guid GrupoAutomovelId, double PrecoDiario, double PrecoDiarioControlado, double PrecoPorKm, double KmLivres, double PrecoporKmExplorado, double PrecoLivre)
    : IRequest<Result<EditarPlanoResponse>>;