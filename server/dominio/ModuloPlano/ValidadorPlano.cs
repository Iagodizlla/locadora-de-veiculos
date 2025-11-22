using FluentValidation;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Dominio.ModuloPlano;

public class ValidadorPlano : AbstractValidator<Plano>
{
    public ValidadorPlano()
    {
        RuleFor(m => m.GrupoAutomovel)
            .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.GrupoAutomovel).SetValidator(new ValidadorGrupoAutomovel());
            });

        RuleFor(plano => plano.PrecoDiario)
            .GreaterThan(0)
            .WithMessage("O valor diário do plano diário deve ser maior que zero.");

        RuleFor(plano => plano.PrecoPorKm)
            .GreaterThan(0)
            .WithMessage("O valor por km deve ser maior que zero.");

        RuleFor(plano => plano.PrecoDiarioControlado)
            .GreaterThan(0)
            .WithMessage("O valor diário do plano controlado deve ser maior que zero.");

        RuleFor(plano => plano.KmLivres)
            .GreaterThan(0)
            .WithMessage("O limite de quilometragem deve ser maior que zero.");

        RuleFor(plano => plano.PrecoPorKmExplorado)
            .GreaterThan(0)
            .WithMessage("O valor por km extrapolado deve ser maior que zero.");

        RuleFor(plano => plano.PrecoLivre)
            .GreaterThan(0)
            .WithMessage("O valor do plano livre deve ser maior que zero.");
    }
}
