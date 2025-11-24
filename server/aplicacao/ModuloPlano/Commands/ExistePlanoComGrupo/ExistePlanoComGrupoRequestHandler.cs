using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.ExistePlanoComGrupo;

public class ExistePlanoComGrupoRequestHandler(
    IRepositorioPlano repositorioPlano
) : IRequestHandler<ExistePlanoComGrupoRequest, Result<ExistePlanoComGrupoResponse>>
{
    public async Task<Result<ExistePlanoComGrupoResponse>> Handle(ExistePlanoComGrupoRequest request, CancellationToken cancellationToken)
    {
        var existe = await repositorioPlano.ExistePlanoComGrupoAsync(request.GrupoAutomovelId);

        var dto = new ExistePlanoComGrupoDto(existe);

        var response = new ExistePlanoComGrupoResponse(dto);

        return Result.Ok(response);
    }
}
