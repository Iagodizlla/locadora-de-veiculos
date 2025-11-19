using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor;

public abstract class CondutorErrorResults
{
    public static Error CnhDuplicadoError(string cnh)
    {
        return new Error("CNH duplicado")
            .CausedBy($"Um condutor com a CNH '{cnh}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
}