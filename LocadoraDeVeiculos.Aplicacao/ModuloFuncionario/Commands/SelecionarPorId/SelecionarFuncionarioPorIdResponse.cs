namespace LocadoreDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;

public record SelecionarFuncionarioPorIdResponse(Guid Id, string Nome, double Salario, DateTimeOffset Admissao);