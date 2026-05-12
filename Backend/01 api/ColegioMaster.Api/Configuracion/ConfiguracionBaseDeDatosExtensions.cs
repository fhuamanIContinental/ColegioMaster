using ColegioMaster.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ColegioMaster.Api.Configuracion;

public static class ConfiguracionBaseDeDatosExtensions
{
    public static IServiceCollection ConfigurarBaseDeDatos(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ColegioConnection");

        services.AddDbContext<ColegioDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
