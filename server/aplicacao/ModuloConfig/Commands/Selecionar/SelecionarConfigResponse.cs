namespace LocadoraDeVeiculos.Aplicacao.ModuloConfig.Commands.Selecionar;

public record SelecionarConfigResponse(
    decimal Gasolina,
    decimal Gas,
    decimal Diesel,
    decimal Alcool
);
