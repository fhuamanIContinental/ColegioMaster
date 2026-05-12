namespace ColegioMaster.Application.Dtos.Crud;

public class UsuarioPlataformaDto
{
    public long Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string ClaveCifrada { get; set; } = string.Empty;
    public int IntentosFallidos { get; set; }
    public DateTime? BloqueadoHasta { get; set; }
    public DateTime? UltimoAcceso { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public string UsuarioCreacion { get; set; } = string.Empty;
    public string? UsuarioModificacion { get; set; }
}

public class CrearUsuarioPlataformaDto
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string ClaveCifrada { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
    public string UsuarioCreacion { get; set; } = string.Empty;
}

public class ActualizarUsuarioPlataformaDto
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string ClaveCifrada { get; set; } = string.Empty;
    public int IntentosFallidos { get; set; }
    public DateTime? BloqueadoHasta { get; set; }
    public DateTime? UltimoAcceso { get; set; }
    public bool Estado { get; set; }
    public string UsuarioModificacion { get; set; } = string.Empty;
}
