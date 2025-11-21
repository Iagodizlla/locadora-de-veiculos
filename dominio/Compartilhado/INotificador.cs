namespace LocadoraDeVeiculos.Dominio.Compartilhado;

public interface INotificador
{
    Task EnviarNotificacaoConfirmacaoAsync(
        string nomePaciente,
        string meioContatoPaciente,
        DateTime inicio,
        DateTime termino
    );
}