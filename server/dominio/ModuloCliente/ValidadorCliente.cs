using FluentValidation;

namespace LocadoraDeVeiculos.Dominio.ModuloCliente;

public class ValidadorCliente : AbstractValidator<Cliente>
{
    public ValidadorCliente()
    {
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Nome).MinimumLength(3)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });

        RuleFor(m => m.Endereco)
            .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Endereco).SetValidator(new ValidadorEndereco());
            });

        RuleFor(m => m.Telefone)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Telefone).MinimumLength(8)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });
    }
}
public class ValidadorEndereco : AbstractValidator<Endereco>
{
    public ValidadorEndereco()
    {
        RuleFor(e => e.Logradouro)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        RuleFor(e => e.Cidade)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        RuleFor(e => e.Estado)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        RuleFor(e => e.Bairro)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        RuleFor(e => e.Numero)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que zero");
    }
}