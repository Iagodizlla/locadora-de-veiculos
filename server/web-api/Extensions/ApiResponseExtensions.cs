using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Extensions;

public static class ApiResponseExtensions
{
    public static IActionResult ToHttpResponse<TResponse>(this Result<TResponse> resultado)
    {
        if (resultado.IsFailed)
        {
            return new JsonResult(resultado.Errors.First().Reasons.Select(r => r.Message).ToList())
            {
                StatusCode = MapErrorStatusCode(resultado.Errors.First())
            };
        }

        return new JsonResult(resultado.Value)
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    private static int MapErrorStatusCode(IError error)
    {
        switch (error.Metadata["ErrorType"]?.ToString())
        {
            case "NotFound":
                return StatusCodes.Status404NotFound;
            case "BadRequest":
                return StatusCodes.Status400BadRequest;
            default:
                return StatusCodes.Status500InternalServerError;
        }
    }
}