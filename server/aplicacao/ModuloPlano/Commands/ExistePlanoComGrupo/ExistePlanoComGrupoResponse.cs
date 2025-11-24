namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano.Commands.ExistePlanoComGrupo;

public record ExistePlanoComGrupoResponse(ExistePlanoComGrupoDto Resultado);

public record ExistePlanoComGrupoDto(bool Existe);