using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAluguel;

public abstract class AluguelErrorResults
{
    public static Error CondutorNaoEncontradoError()
    {
        return new Error("Condutor requisitado não encontrado")
            .CausedBy("Não foi possível obter o Condutor informado na requisição")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error AutomovelNaoEncontradoError()
    {
        return new Error("Automóvel requisitado não encontrado")
            .CausedBy("Não foi possível obter o Automóvel informado na requisição")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error ClienteNaoEncontradoError()
    {
        return new Error("Cliente requisitado não encontrado")
            .CausedBy("Não foi possível obter o Cliente informado na requisição")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error PlanoNaoEncontradoError()
    {
        return new Error("Plano requisitado não encontrado")
            .CausedBy("Não foi possível obter o Plano informado na requisição")
            .WithMetadata("ErrorType", "BadRequest");
    }
}