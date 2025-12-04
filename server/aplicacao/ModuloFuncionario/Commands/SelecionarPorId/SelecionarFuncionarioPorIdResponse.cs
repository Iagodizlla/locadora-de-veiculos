namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;

public record SelecionarFuncionarioPorIdResponse(Guid Id, string UserName, double Salario, DateTimeOffset Admissao);