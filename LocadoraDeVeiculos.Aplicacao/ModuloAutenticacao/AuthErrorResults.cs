using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao;

public abstract class AuthErrorResults
{
    public static Error ConfirmacaoEmailPendenteError(string email)
    {
        return new Error("Confirmação de email pendente")
            .CausedBy($"A confirmação do email \"{email}\" está pendente")
            .WithMetadata("ErrorType", "BadRequest");
    }
    
    public static Error CredenciaisIncorretasError()
    {
        return new Error("Credenciais incorretas")
            .CausedBy("O login ou a senha estão incorretos")
            .WithMetadata("ErrorType", "BadRequest");
    }
    
    public static Error UsuarioBloqueadoError()
    {
        return new Error("Usuário bloqueado")
            .CausedBy("O acesso para este usuário foi bloqueado, tente mais tarde")
            .WithMetadata("ErrorType", "BadRequest");
    }
    
    public static Error UsuarioNaoEncontradoError(string nome)
    {
        return new Error("Usuário não encontrado")
            .CausedBy($"O usuário com o login '{nome}' não foi encontrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
}