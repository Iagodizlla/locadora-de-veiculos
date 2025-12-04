using FluentValidation;
namespace LocadoraDeVeiculos.Dominio.ModuloFuncionario;

public class ValidadorFuncionario : AbstractValidator<Funcionario>
{
    public ValidadorFuncionario()
    {
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Nome).MinimumLength(3)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });

        RuleFor(m => m.Salario)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que zero");

        RuleFor(m => m.Admissao)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("O campo {PropertyName} não pode ser uma data futura");
    }
}