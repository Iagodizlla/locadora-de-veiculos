using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Editar;

public record EditarPlanoPartialRequest(ETipoPlano TipoPlano, Guid GrupoAutomovelId, double? PrecoDiario, double? PrecoPorKm, double? KmLivres, double? PrecoporKmExplorado, double? PrecoLivre);

public record EditarPlanoRequest(Guid Id, ETipoPlano TipoPlano, Guid GrupoAutomovelId, double? PrecoDiario, double? PrecoPorKm, double? KmLivres, double? PrecoporKmExplorado, double? PrecoLivre)
    : IRequest<Result<EditarPlanoResponse>>;