using ColegioMaster.Api.Model.Auth;
using ColegioMaster.Application.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.RequestResponse;
using Swashbuckle.AspNetCore.Annotations;

namespace ColegioMaster.Api.Controllers;

/// <summary>
/// Controlador de autenticación. Gestiona el inicio de sesión y la emisión de tokens JWT.
/// </summary>
[SwaggerTag("Gestiona la autenticación de usuarios y la emisión de tokens JWT.")]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAutenticacionService autenticacionService;

    public AuthController(IAutenticacionService autenticacionService)
    {
        this.autenticacionService = autenticacionService;
    }

    /// <summary>
    /// Autentica un usuario y retorna un token JWT.
    /// </summary>
    /// <param name="request">Credenciales del usuario (nombre de usuario y clave).</param>
    /// <returns>Token JWT junto con información del usuario autenticado.</returns>
    /// <remarks>
    /// Este endpoint es público y no requiere autenticación previa.
    /// </remarks>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(GeneralResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GeneralResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralResponse<LoginResponse>), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GeneralResponse<LoginResponse>>> Login([FromBody] LoginRequest request)
    {
        var resultado = await autenticacionService.IniciarSesionAsync(request.NombreUsuario, request.Clave);

        if (!resultado.Exitoso)
        {
            return Unauthorized(new GeneralResponse<LoginResponse>
            {
                Success = false,
                TitleMessage = resultado.TituloMensaje,
                TextMessage = resultado.TextoMensaje,
                ShowAlert = true
            });
        }

        return Ok(new GeneralResponse<LoginResponse>
        {
            Success = true,
            TitleMessage = resultado.TituloMensaje,
            TextMessage = resultado.TextoMensaje,
            ShowAlert = false,
            Content = new LoginResponse
            {
                Token = resultado.Token!,
                ExpiraEnUtc = resultado.ExpiraEnUtc!.Value,
                NombreUsuario = resultado.NombreUsuario!,
                NombreCompleto = resultado.NombreCompleto!
            }
        });
    }
}
