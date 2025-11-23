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

    public static Error ClienteNaoEncontradoError(Guid clienteId)
    {
        return new Error("Cliente não encontrado")
            .CausedBy($"Não foi possível encontrar o cliente com Id '{clienteId}' associado ao condutor")
            .WithMetadata("ErrorType", "BadRequest");
    }
}