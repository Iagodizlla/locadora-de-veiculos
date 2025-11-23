using FluentValidation;

namespace LocadoraDeVeiculos.Dominio.ModuloAluguel;

public class ValidadorAluguel : AbstractValidator<Aluguel>
{
    public ValidadorAluguel()
    {
        #region Datas
        RuleFor(a => a.DataSaida)
            .NotNull()
            .WithMessage("A data de inicio é obrigatória")
            .LessThan(a => a.DataRetornoPrevista)
            .WithMessage("A data de início deve ser anterior à data de fim prevista.");

        RuleFor(a => a.DataRetornoPrevista)
            .NotNull()
            .WithMessage("A data de fim prevista é obrigatória")
            .GreaterThanOrEqualTo(a => a.DataSaida)
            .WithMessage("A data de retorno prevista deve ser maior ou igual à data de saída.");

        RuleFor(a => a.DataDevolucao)
            .GreaterThanOrEqualTo(a => a.DataSaida)
            .When(a => a.DataDevolucao.HasValue)
            .WithMessage("A data de devolução deve ser maior ou igual à data de saída quando fornecida.");
        #endregion
        #region Distancia e Combustível
        RuleFor(a => a.QuilometragemInicial)
            .NotNull()
            .WithMessage("A quilometragem inicial é obrigatória")
            .GreaterThanOrEqualTo(0)
            .WithMessage("A quilometragem inicial deve ser maior ou igual a zero.");

        RuleFor(a => a.QuilometragemFinal)
            .GreaterThanOrEqualTo(a => a.QuilometragemInicial)
            .When(a => a.QuilometragemFinal.HasValue)
            .WithMessage("A quilometragem final deve ser maior ou igual à quilometragem inicial quando fornecida.");

        RuleFor(a => a.NivelCombustivelNaSaida)
            .NotNull()
            .WithMessage("O nível de combustivel inicial é obrigatório");

        RuleFor(a => a.NivelCombustivelNaDevolucao)
            .LessThanOrEqualTo(a => a.NivelCombustivelNaSaida)
            .When(a => a.NivelCombustivelNaDevolucao.HasValue)
            .WithMessage("O nível de combustível na devolução deve estar menos ou igual quando o automóvel saiu.");
        #endregion
        #region Seguro
        RuleFor(a => a.ValorSeguroPorDia)
            .GreaterThanOrEqualTo(0)
            .When(a => a.ValorSeguroPorDia.HasValue)
            .WithMessage("O valor do seguro por dia deve ser maior ou igual a zero quando fornecido.");
        #endregion
        #region Entidades
        RuleFor(a => a.Condutor)
            .NotNull()
            .WithMessage("O condutor é obrigatório.");

        RuleFor(a => a.Automovel)
            .NotNull()
            .WithMessage("O automóvel é obrigatório.");

        RuleFor(a => a.Plano)
            .NotNull()
            .WithMessage("O plano é obrigatório.");

        RuleFor(a => a.Taxas)
            .NotNull()
            .WithMessage("As taxas são obrigatórias.");

        RuleFor(a => a.Cliente)
            .NotNull()
            .WithMessage("O cliente é obrigatório.");
        #endregion
    }
}
