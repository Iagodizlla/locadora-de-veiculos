using FluentValidation;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Dominio.ModuloAutomovel;

public class ValidadorAutomovel : AbstractValidator<Automovel>
{
    public ValidadorAutomovel()
    {
        RuleFor(m => m.Placa)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Placa).MinimumLength(3)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });

        RuleFor(m => m.Modelo)
           .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
           .DependentRules(() =>
           {
               RuleFor(m => m.Modelo).MinimumLength(3)
                   .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
           });

        RuleFor(m => m.Marca)
           .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
           .DependentRules(() =>
           {
               RuleFor(m => m.Marca).MinimumLength(3)
                   .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
           });

        RuleFor(m => m.Cor)
           .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
           .DependentRules(() =>
           {
               RuleFor(m => m.Cor).MinimumLength(3)
                   .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
           });

        RuleFor(m => m.Foto)
           .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
           .DependentRules(() =>
           {
               RuleFor(m => m.Foto).MinimumLength(3)
                   .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
           });

        RuleFor(m => m.Ano)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que zero");

        RuleFor(m => m.CapacidadeTanque)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que zero");

        RuleFor(m => m.GrupoAutomovelId)
            .NotNull().WithMessage("O campo Grupo é obrigatório")

        RuleFor(m => m.Combustivel)
            .IsInEnum().WithMessage("O campo {PropertyName} é obrigatório");
    }
}