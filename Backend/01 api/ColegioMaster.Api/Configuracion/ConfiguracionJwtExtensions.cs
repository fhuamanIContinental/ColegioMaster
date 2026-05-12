using System.Text;
using ColegioMaster.Application.Configuracion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ColegioMaster.Api.Configuracion;

public static class ConfiguracionJwtExtensions
{
    public static IServiceCollection ConfigurarJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConfiguracionJwt>(configuration.GetSection("Jwt"));

        var jwtSection = configuration.GetSection("Jwt");
        var claveJwt = jwtSection["ClaveSecreta"] ?? throw new InvalidOperationException("Falta configurar Jwt:ClaveSecreta");
        var issuerJwt = jwtSection["Issuer"];
        var audienceJwt = jwtSection["Audience"];

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuerJwt,
                    ValidAudience = audienceJwt,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveJwt)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

        return services;
    }
}
