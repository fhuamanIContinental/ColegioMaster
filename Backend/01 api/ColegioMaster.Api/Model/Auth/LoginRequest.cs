namespace ColegioMaster.Api.Model.Auth;

public class LoginRequest
{
    public string NombreUsuario { get; set; } = string.Empty;

    public string Clave { get; set; } = string.Empty;
}
