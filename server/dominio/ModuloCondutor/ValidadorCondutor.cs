using FluentValidation;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
namespace LocadoraDeVeiculos.Dominio.ModuloCondutor;

public class ValidadorCondutor : AbstractValidator<Condutor>
{
    public ValidadorCondutor()
    {
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Nome).MinimumLength(3)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });

        RuleFor(m => m.Cpf)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Cpf).MinimumLength(11)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });

        RuleFor(m => m.Telefone)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Telefone).MinimumLength(8)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });

        RuleFor(m => m.Cnh)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Cnh).MinimumLength(11)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });

        RuleFor(m => m.ValidadeCnh)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .GreaterThan(DateTime.Now).WithMessage("O campo {PropertyName} deve ser uma data futura");

        RuleFor(m => m.Categoria)
            .IsInEnum().WithMessage("O campo {PropertyName} é obrigatório");

        When(m => m.ECliente == false, () =>
        {
            RuleFor(m => m.Cliente)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
                .SetValidator(new ValidadorCliente());
        });
    }       
}