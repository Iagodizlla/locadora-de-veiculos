using FluentValidation;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;

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
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Endereco).MinimumLength(5)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
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

public class ValidadorPessoaFisica : AbstractValidator<PessoaFisica>
{
    public ValidadorPessoaFisica()
    {
        RuleFor(m => m.Cpf)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Cpf).MinimumLength(11)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });
        RuleFor(m => m.Rg)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Rg).MinimumLength(7)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });
        RuleFor(m => m.Cnh)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Cnh).MinimumLength(11)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });
    }
}

public class ValidadorPessoaJuridica : AbstractValidator<PessoaJuridica>
{
    public ValidadorPessoaJuridica()
    {
        RuleFor(m => m.Cnpj)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.Cnpj).MinimumLength(14)
                    .WithMessage("O campo {PropertyName} deve conter no mínimo {MinLength} caracteres");
            });
        RuleFor(m => m.RepresentanteLegal)
            .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
            .DependentRules(() =>
            {
                RuleFor(m => m.RepresentanteLegal).SetValidator(new ValidadorPessoaFisica());
            });
    }
}