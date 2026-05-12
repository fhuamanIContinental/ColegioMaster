using ColegioMaster.Application.Utilidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ColegioMaster.Api.Configuracion;

public static class ConfiguracionCifradoExtensions
{
    public static IServiceCollection ConfigurarCifradoAes(this IServiceCollection services, IConfiguration configuration)
    {
        var claveCifrado = configuration["CifradoAes:Clave"]
            ?? throw new InvalidOperationException("Falta configurar CifradoAes:Clave");
        var ivCifrado = configuration["CifradoAes:Iv"]
            ?? throw new InvalidOperationException("Falta configurar CifradoAes:Iv");

        services.AddSingleton(new CifradoAes(claveCifrado, ivCifrado));

        return services;
    }
}
