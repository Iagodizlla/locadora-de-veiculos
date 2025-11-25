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

    public static Error TelefoneDuplicadoError(string telefone)
    {
        return new Error("Telefone duplicado")
            .CausedBy($"Um condutor com o telefone '{telefone}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error CpfDuplicadoError(string cpf)
    {
        return new Error("CPF duplicado")
            .CausedBy($"Um condutor com o CPF '{cpf}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
    public static Error CondutorComAluguelNaoFinalizadoError()
    {
        return new Error("Não é possível excluir este condutor no momento.")
            .CausedBy("O condutor está vinculado a um aluguel que ainda não foi concluído.")
            .WithMetadata("ErrorType", "BadRequest");
    }
}