using ColegioMaster.Application.Servicios;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Infrastructure.Repositorios;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ColegioMaster.Api.Configuracion;

public static class ConfiguracionDependenciasExtensions
{
    public static IServiceCollection ConfigurarDependenciasAplicacion(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositorioCrud<>), typeof(RepositorioCrud<>));

        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IClienteSuscripcionService, ClienteSuscripcionService>();
        services.AddScoped<IEstadoClienteService, EstadoClienteService>();
        services.AddScoped<IEstadoSuscripcionService, EstadoSuscripcionService>();
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<IUsuarioPlataformaService, UsuarioPlataformaService>();

        services.AddScoped<IUsuarioPlataformaRepositorio, UsuarioPlataformaRepositorio>();
        services.AddScoped<IAutenticacionService, AutenticacionService>();

        return services;
    }
}
