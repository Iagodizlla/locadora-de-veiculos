using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.DTOs;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.Commands.Autenticar;

public record AutenticarUsuarioRequest(string UserName, string Password) : IRequest<Result<TokenResponse>>;