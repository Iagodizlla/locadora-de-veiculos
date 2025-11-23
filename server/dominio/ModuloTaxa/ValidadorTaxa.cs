using FluentValidation;

namespace LocadoraDeVeiculos.Dominio.ModuloTaxa;

public class ValidadorTaxa : AbstractValidator<Taxa>
{
    public ValidadorTaxa()
    {
        RuleFor(taxa => taxa.Nome)
            .NotEmpty().WithMessage("O nome da taxa é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da taxa não pode exceder 100 caracteres.");

        RuleFor(taxa => taxa.Preco)
            .GreaterThan(0).WithMessage("O preço da taxa deve ser maior que zero.");

        RuleFor(taxa => taxa.Servico)
            .IsInEnum().WithMessage("O serviço da taxa é inválido.");
    }
}
