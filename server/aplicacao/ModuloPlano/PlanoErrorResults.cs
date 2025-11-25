using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloPlano;

public abstract class PlanoErrorResults
{
    public static Error GrupoNaoEncontradoError()
    {
        return new Error("Grupo de Automóvel requisitado não encontrado")
            .CausedBy("Não foi possível obter o Grupa de automóvel informado na requisição")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error GrupoDuplicadoError(string nome)
    {
        return new Error("Grupo duplicado")
            .CausedBy($"Um grupo com o nome '{nome}' já foi cadastrado com outro plano")
            .WithMetadata("ErrorType", "BadRequest");
    }
    public static Error PlanoPossuiAlugueisError()
    {
        return new Error("Plano possui aluguel vinculado")
            .CausedBy("Não é possível excluir um plano que ainda possui alguel cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
}