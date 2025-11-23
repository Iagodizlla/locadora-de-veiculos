using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa;

public abstract class TaxaErrorResults
{
    public static Error NomeDuplicadoError(string nome)
    {
        return new Error("Nome duplicado")
            .CausedBy($"Um grupo de automóvel com o nome '{nome}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
}