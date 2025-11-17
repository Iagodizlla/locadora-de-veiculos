using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace LocadoraDeVeiculos.WebApi.Config;

public static class GlobalExceptionHandlerConfig
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(builder =>
        {
            builder.Run(async httpContext =>
            {
                var gerenciadorExcecoes = httpContext.Features.Get<IExceptionHandlerFeature>();

                if (gerenciadorExcecoes is null) return;

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var objeto = new
                {
                    Sucesso = false,
                    Erros = new string[] { "Erro interno do servidor" }
                };

                var respostaJson = JsonSerializer.Serialize(objeto);

                await httpContext.Response.WriteAsync(respostaJson);
            });
        });
    }
}