using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel;

public abstract class GrupoAutomovelErrorResults
{
    public static Error NomeDuplicadoError(string nome)
    {
        return new Error("Nome duplicado")
            .CausedBy($"Um médico com o nome '{nome}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
}