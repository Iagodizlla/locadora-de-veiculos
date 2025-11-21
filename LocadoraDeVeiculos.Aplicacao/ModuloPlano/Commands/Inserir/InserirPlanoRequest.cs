using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.Inserir;

public record InserirPlanoRequest(ETipoPlano TipoPlano, Guid GrupoAutomovelId, double? PrecoDiario, double? PrecoPorKm, double? KmLivres, double? PrecoporKmExplorado, double? PrecoLivre)
    : IRequest<Result<InserirPlanoResponse>>;
