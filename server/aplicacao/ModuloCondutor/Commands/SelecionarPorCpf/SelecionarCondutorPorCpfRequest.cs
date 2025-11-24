using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCondutor.Commands.SelecionarPorCpf;

public record SelecionarCondutorPorCpfRequest(string Cpf) : IRequest<Result<SelecionarCondutorPorCpfResponse>>;