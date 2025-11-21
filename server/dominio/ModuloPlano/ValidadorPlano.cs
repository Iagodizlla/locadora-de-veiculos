using FluentValidation;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Dominio.ModuloPlano;

public class ValidadorPlano : AbstractValidator<Plano>
{
    public ValidadorPlano()
    {
        RuleFor(plano => plano.TipoPlano)
            .IsInEnum()
            .WithMessage("O tipo do plano é inválido.");

        RuleFor(plano => plano)
            .Must(plano => plano.TipoPlano == ETipoPlano.Diario ||
                           plano.TipoPlano == ETipoPlano.Controlado ||
                           plano.TipoPlano == ETipoPlano.Livre)
            .WithMessage("O tipo do plano deve ser Diário, Controlado ou Livre.");

        RuleFor(m => m.GrupoAutomovel)
            .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.GrupoAutomovel).SetValidator(new ValidadorGrupoAutomovel());
            });

        RuleFor(plano => plano.PrecoDiario)
            .GreaterThan(0).When(plano => plano.TipoPlano != ETipoPlano.Livre)
            .WithMessage("O valor diário deve ser maior que zero para o plano Diário ou Controlado.");

        RuleFor(plano => plano.PrecoPorKm)
            .GreaterThan(0).When(plano => plano.TipoPlano == ETipoPlano.Diario)
            .WithMessage("O valor por km deve ser maior que zero para o plano Diário.");

        RuleFor(plano => plano.KmLivres)
            .GreaterThan(0).When(plano => plano.TipoPlano == ETipoPlano.Controlado)
            .WithMessage("O limite de quilometragem deve ser maior que zero para o plano Controlado.");

        RuleFor(plano => plano.PrecoPorKmExplorado)
            .GreaterThan(0).When(plano => plano.TipoPlano == ETipoPlano.Controlado)
            .WithMessage("O valor por km explorado deve ser maior que zero para o plano Controlado.");

        RuleFor(plano => plano.PrecoLivre)
            .GreaterThan(0).When(plano => plano.TipoPlano == ETipoPlano.Livre)
            .WithMessage("O valor do plano livre deve ser maior que zero para o plano Livre.");
    }
}
