using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Inserir;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAutomovel;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCondutor;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloFuncionario;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloCliente;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloPlano;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloTaxa;
using LocadoraDeVeiculos.WebApi.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloConfig;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloConfig;

namespace LocadoraDeVeiculos.WebApi;

public static class DependencyInjection
{
    public static void ConfigureDbContext(
        this IServiceCollection services,
        IConfiguration config,
        IWebHostEnvironment environment
    )
    {
        var connectionString = config["SQL_CONNECTION_STRING"];

        if (connectionString == null)
            throw new ArgumentNullException("'SQL_CONNECTION_STRING' não foi fornecida para o ambiente.");

        services.AddDbContext<IContextoPersistencia, LocadoraVeiculoDbContext>(optionsBuilder =>
        {
            if (!environment.IsDevelopment())
                optionsBuilder.EnableSensitiveDataLogging(false);

            optionsBuilder.UseSqlServer(connectionString, dbOptions =>
            {
                dbOptions.EnableRetryOnFailure();
            });
        });
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositorioFuncionario, RepositorioFuncionarioEmOrm>();
        services.AddScoped<IRepositorioGrupoAutomovel, RepositorioGrupoAutomovelEmOrm>();
        services.AddScoped<IRepositorioAutomovel, RepositorioAutomovelEmOrm>();
        services.AddScoped<IRepositorioCliente, RepositorioClienteEmOrm>();
        services.AddScoped<IRepositorioPlano, RepositorioPlanoEmOrm>();
        services.AddScoped<IRepositorioCondutor, RepositorioCondutorEmOrm>();
        services.AddScoped<IRepositorioTaxa, RepositorioTaxaEmOrm>();
        services.AddScoped<IRepositorioAluguel, RepositorioAluguelEmOrm>();
        services.AddScoped<IRepositorioConfig, RepositorioConfigEmOrm>();
    }

    public static void ConfigureControllersWithFilters(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ResponseWrapperFilter>();
        }).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }

    public static void ConfigureOpenApiAuthHeaders(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "LocadoraVeiculo API", Version = "v1" });

            options.MapType<TimeSpan>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "time-span",
                Example = new Microsoft.OpenApi.Any.OpenApiString("00:00:00")
            });

            options.MapType<Guid>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "guid",
                Example = new Microsoft.OpenApi.Any.OpenApiString("00000000-0000-0000-0000-000000000000")
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Informe o token JWT no padrão {Bearer token}",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
    }

    public static void ConfigureCorsPolicy(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        IConfiguration configuration
    )
    {
        services.AddCors(options =>
        {
            if (environment.IsDevelopment())
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            }
            else
            {
                var origensPermitidasString = configuration["CORS_ALLOWED_ORIGINS"];

                if (string.IsNullOrWhiteSpace(origensPermitidasString))
                    throw new Exception("A variável de ambiente \"CORS_ALLOWED_ORIGINS\" não foi fornecida.");

                var origensPermitidas = origensPermitidasString
                    .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(x => x.TrimEnd('/'))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToArray();

                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .WithOrigins(origensPermitidas)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            }
        });
    }

    public static void ConfigureFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ValidadorFuncionario>();
    }

    public static void ConfigureSerilog(this IServiceCollection services, ILoggingBuilder logging, IConfiguration config)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.newrelic.com/log/v1",
                applicationName: "locadora-veiculo-api",
                licenseKey: config["NEWRELIC_LICENSE_KEY"]
            )
            .CreateLogger();

        logging.ClearProviders();

        services.AddLogging(builder => builder.AddSerilog(dispose: true));
    }

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<InserirFuncionarioRequest>();
        });
    }
}