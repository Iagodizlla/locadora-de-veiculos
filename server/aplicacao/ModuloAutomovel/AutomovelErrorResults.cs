using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutomovel;

public abstract class AutomovelErrorResults
{
    public static Error PlacaDuplicadoError(string placa)
    {
        return new Error("Placa duplicado")
            .CausedBy($"Um automóvel com a placa '{placa}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error GrupoNaoEncontradoError()
    {
        return new Error("Grupo de Automóvel requisitado não encontrado")
            .CausedBy("Não foi possível obter o Grupa de automóvel informado na requisição")
            .WithMetadata("ErrorType", "BadRequest");
    }
    public static Error VeiculoComAluguelNaoFinalizadoError()
    {
        return new Error("Não é possível editar este veículo no momento.")
            .CausedBy("O veículo está vinculado a um aluguel que ainda não foi concluído.")
            .WithMetadata("ErrorType", "BadRequest");
    }
}