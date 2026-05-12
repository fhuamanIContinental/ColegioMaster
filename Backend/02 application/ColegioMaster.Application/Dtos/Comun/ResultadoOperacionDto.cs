namespace ColegioMaster.Application.Dtos.Comun;

public class ResultadoOperacionDto<T>
{
    public bool Exitoso { get; set; }
    public string TituloMensaje { get; set; } = string.Empty;
    public string TextoMensaje { get; set; } = string.Empty;
    public T? Contenido { get; set; }
}

public class ResultadoOperacionDto
{
    public bool Exitoso { get; set; }
    public string TituloMensaje { get; set; } = string.Empty;
    public string TextoMensaje { get; set; } = string.Empty;
}
