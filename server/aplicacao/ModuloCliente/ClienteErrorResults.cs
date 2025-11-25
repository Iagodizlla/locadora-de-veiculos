using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente;

public abstract class ClienteErrorResults
{
    public static Error NomeDuplicadoError(string nome)
    {
        return new Error("Nome duplicado")
            .CausedBy($"Um cliente com o nome '{nome}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error TelefoneDuplicadoError(string telefone)
    { 
        return new Error("Telefone duplicado")
            .CausedBy($"Um cliente com o telefone '{telefone}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error DocumentoDuplicadoError(string documento)
    {
        return new Error("Documento duplicado")
            .CausedBy($"Um cliente com o documento '{documento}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error CnhDuplicadoError(string cnh)
    {
        return new Error("CNH duplicada")
            .CausedBy($"Um cliente com a CNH '{cnh}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
    public static Error ClienteComAluguelNaoFinalizadoError()
    {
        return new Error("Não é possível editar ou excluir este cliente no momento.")
            .CausedBy("O cliente está vinculado a um aluguel que ainda não foi concluído.")
            .WithMetadata("ErrorType", "BadRequest");
    }
}