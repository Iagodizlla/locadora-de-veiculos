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
    public static Error TaxaComAluguelNaoFinalizadoError()
    {
        return new Error("Não é possível excluir esta taxa no momento.")
            .CausedBy("A taxa está vinculado a um aluguel que ainda não foi concluído.")
            .WithMetadata("ErrorType", "BadRequest");
    }
}