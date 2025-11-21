using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario;

public abstract class FuncionarioErrorResults
{
    public static Error NomeDuplicadoError(string nome)
    {
        return new Error("Nome duplicado")
            .CausedBy($"Um médico com o nome '{nome}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
}