using FluentResults;
using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.DTOs;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.Commands.Registrar;

public record RegistrarUsuarioRequest(string UserName, string Email, string Password) 
    : IRequest<Result<TokenResponse>>;