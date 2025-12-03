using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

public class Cargo : IdentityRole<Guid>
{
}

public enum ECargo
{
    Empresa,
    [Display(Name = "Funcionário")] Funcionario
}