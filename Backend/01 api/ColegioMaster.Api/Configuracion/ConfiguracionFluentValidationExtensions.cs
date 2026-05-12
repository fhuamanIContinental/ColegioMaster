using ColegioMaster.Api.Model.Auth.Validadores;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Model.RequestResponse;

namespace ColegioMaster.Api.Configuracion;

public static class ConfiguracionFluentValidationExtensions
{
    public static IServiceCollection ConfigurarFluentValidation(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errores = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .ToList();

                var mensaje = errores.Count > 0
                    ? string.Join(" | ", errores)
                    : "Solicitud invalida.";

                return new BadRequestObjectResult(new GeneralResponse
                {
                    Success = false,
                    TitleMessage = "Solicitud invalida",
                    TextMessage = mensaje,
                    ShowAlert = true
                });
            };
        });

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

        return services;
    }
}
