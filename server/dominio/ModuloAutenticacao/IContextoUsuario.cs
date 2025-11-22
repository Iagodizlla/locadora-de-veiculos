namespace LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

public interface  IContextoUsuario
{
    Guid? UsuarioId { get; }
    Guid GetUserId() => this.UsuarioId.GetValueOrDefault();
    bool IsInRole(string nome);
}
