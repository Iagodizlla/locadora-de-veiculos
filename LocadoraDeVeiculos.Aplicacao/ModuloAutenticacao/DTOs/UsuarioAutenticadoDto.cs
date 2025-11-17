namespace LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.DTOs;

public class UsuarioAutenticadoDto
{
    public required Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
}