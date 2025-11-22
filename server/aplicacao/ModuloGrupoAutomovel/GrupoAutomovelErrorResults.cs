using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel;

public abstract class GrupoAutomovelErrorResults
{
    public static Error NomeDuplicadoError(string nome)
    {
        return new Error("Nome duplicado")
            .CausedBy($"Um grupo de automóvel com o nome '{nome}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
    public static Error GrupoPossuiAutomoveisError()
    {
        return new Error("Grupo possui automóveis vinculados")
            .CausedBy("Não é possível excluir um grupo de automóvel que ainda possui automóveis cadastrados")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error GrupoPossuiPlanosError()
    {
        return new Error("Grupo possui planos vinculados")
            .CausedBy("Não é possível excluir um grupo de automóvel que ainda possui planos cadastrados")
            .WithMetadata("ErrorType", "BadRequest");
    }
}