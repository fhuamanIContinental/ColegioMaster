using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ColegioMaster.Application.Configuracion;
using ColegioMaster.Application.Dtos.Auth;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Application.Utilidades;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ColegioMaster.Application.Servicios;

public class AutenticacionService : IAutenticacionService
{
    private const int MaximoIntentosPermitidos = 5;
    private static readonly TimeSpan TiempoBloqueo = TimeSpan.FromMinutes(15);

    private readonly IUsuarioPlataformaRepositorio usuarioPlataformaRepositorio;
    private readonly CifradoAes cifradoAes;
    private readonly ConfiguracionJwt configuracionJwt;

    public AutenticacionService(
        IUsuarioPlataformaRepositorio usuarioPlataformaRepositorio,
        CifradoAes cifradoAes,
        IOptions<ConfiguracionJwt> configuracionJwt)
    {
        this.usuarioPlataformaRepositorio = usuarioPlataformaRepositorio;
        this.cifradoAes = cifradoAes;
        this.configuracionJwt = configuracionJwt.Value;
    }

    public async Task<ResultadoAutenticacionDto> IniciarSesionAsync(string nombreUsuario, string claveTextoPlano)
    {
        var usuario = await usuarioPlataformaRepositorio.ObtenerPorNombreUsuarioAsync(nombreUsuario);

        if (usuario is null || !usuario.Estado)
        {
            return ConstruirRespuestaFallida("Acceso denegado", "Usuario o clave no validos.");
        }

        if (usuario.BloqueadoHasta.HasValue && usuario.BloqueadoHasta.Value > DateTime.UtcNow)
        {
            return ConstruirRespuestaFallida("Usuario bloqueado", "El usuario se encuentra bloqueado temporalmente.");
        }

        var claveCifradaEntrada = cifradoAes.Cifrar(claveTextoPlano);
        //if (!string.Equals(usuario.ClaveCifrada, claveCifradaEntrada, StringComparison.Ordinal))
        //{
        //    usuario.IntentosFallidos++;

        //    if (usuario.IntentosFallidos >= MaximoIntentosPermitidos)
        //    {
        //        usuario.BloqueadoHasta = DateTime.UtcNow.Add(TiempoBloqueo);
        //        usuario.IntentosFallidos = 0;
        //    }

        //    usuario.FechaModificacion = DateTime.UtcNow;
        //    usuario.UsuarioModificacion = "SISTEMA";
        //    await usuarioPlataformaRepositorio.ActualizarAsync(usuario);

        //    return ConstruirRespuestaFallida("Credenciales invalidas", "Usuario o clave no validos.");
        //}

        usuario.IntentosFallidos = 0;
        usuario.BloqueadoHasta = null;
        usuario.UltimoAcceso = DateTime.UtcNow;
        usuario.FechaModificacion = DateTime.UtcNow;
        usuario.UsuarioModificacion = "SISTEMA";
        await usuarioPlataformaRepositorio.ActualizarAsync(usuario);

        var expiracionUtc = DateTime.UtcNow.AddMinutes(configuracionJwt.MinutosExpiracion);
        var token = GenerarToken(usuario.Id, usuario.Correo, usuario.Nombres, usuario.Apellidos, expiracionUtc);

        return new ResultadoAutenticacionDto
        {
            Exitoso = true,
            TituloMensaje = "Acceso concedido",
            TextoMensaje = "Inicio de sesion realizado correctamente.",
            Token = token,
            ExpiraEnUtc = expiracionUtc,
            NombreUsuario = usuario.Correo,
            NombreCompleto = $"{usuario.Nombres} {usuario.Apellidos}".Trim()
        };
    }

    private string GenerarToken(long idUsuario, string nombreUsuario, string nombres, string apellidos, DateTime expiraEnUtc)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, idUsuario.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, nombreUsuario),
            new(JwtRegisteredClaimNames.GivenName, nombres),
            new(JwtRegisteredClaimNames.FamilyName, apellidos),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("password", "miPassword"),
        };

        var credenciales = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracionJwt.ClaveSecreta)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuracionJwt.Issuer,
            audience: configuracionJwt.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiraEnUtc,
            signingCredentials: credenciales);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static ResultadoAutenticacionDto ConstruirRespuestaFallida(string titulo, string mensaje)
    {
        return new ResultadoAutenticacionDto
        {
            Exitoso = false,
            TituloMensaje = titulo,
            TextoMensaje = mensaje
        };
    }
}
