using FluentValidation;

namespace LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

public class ValidadorGrupoAutomovel : AbstractValidator<GrupoAutomovel>
{
    public ValidadorGrupoAutomovel()
    {
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Nome).MinimumLength(3)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });
    }
}