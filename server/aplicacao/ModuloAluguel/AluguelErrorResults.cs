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
    public static Error CondutorInvalidoParaClientePJError()
    {
        return new Error("Condutor inválido para Cliente do tipo PJ")
            .CausedBy("Clientes PJ só podem selecionar condutores do tipo Não Cliente")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error CondutorInvalidoParaClientePFError()
    {
        return new Error("Condutor inválido para Cliente do tipo PF")
            .CausedBy("Clientes PF só podem selecionar condutores do tipo Cliente")
            .WithMetadata("ErrorType", "BadRequest");
    }

    public static Error GruposAutomovelIncompativeisError()
    {
        return new Error("O Automóvel e o Plano pertencem a grupos diferentes")
            .CausedBy("O grupo do Automóvel selecionado não corresponde ao grupo do Plano informado")
            .WithMetadata("ErrorType", "BadRequest");
    }
    public static Error AluguelNaoPodeSerExcluidoError()
    {
        return new Error("Não é possível excluir este aluguel")
            .CausedBy("O aluguel já está finalizado e não pode ser removido")
            .WithMetadata("ErrorType", "BadRequest");
    }
}