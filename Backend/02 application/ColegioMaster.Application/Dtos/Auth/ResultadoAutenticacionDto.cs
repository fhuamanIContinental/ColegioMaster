namespace ColegioMaster.Application.Dtos.Auth;

public class ResultadoAutenticacionDto
{
    public bool Exitoso { get; set; }
    public string TituloMensaje { get; set; } = string.Empty;
    public string TextoMensaje { get; set; } = string.Empty;
    public string? Token { get; set; }
    public DateTime? ExpiraEnUtc { get; set; }
    public string? NombreUsuario { get; set; }
    public string? NombreCompleto { get; set; }
}
