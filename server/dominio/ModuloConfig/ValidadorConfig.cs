using FluentValidation;

namespace LocadoraDeVeiculos.Dominio.ModuloConfig;

public class ValidadorConfig : AbstractValidator<Config>
{
    public ValidadorConfig()
    {
        RuleFor(x => x.Gasolina)
            .GreaterThanOrEqualTo(0).WithMessage("Preço da gasolina inválido");

        RuleFor(x => x.Gas)
            .GreaterThanOrEqualTo(0).WithMessage("Preço do gás inválido");

        RuleFor(x => x.Diessel)
            .GreaterThanOrEqualTo(0).WithMessage("Preço do diesel inválido");

        RuleFor(x => x.Alcool)
            .GreaterThanOrEqualTo(0).WithMessage("Preço do álcool inválido");
    }
}
