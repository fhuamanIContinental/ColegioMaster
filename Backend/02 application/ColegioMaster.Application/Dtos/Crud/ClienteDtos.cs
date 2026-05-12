namespace ColegioMaster.Application.Dtos.Crud;

public class ClienteDto
{
    public int Id { get; set; }
    public string Ruc { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string RazonSocial { get; set; } = string.Empty;
    public string NombreComercial { get; set; } = string.Empty;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? CorreoContacto { get; set; }
    public string ServidorSql { get; set; } = string.Empty;
    public string BdNombre { get; set; } = string.Empty;
    public string? BdUsuario { get; set; }
    public string? BdPasswordCifrada { get; set; }
    public int IdEstado { get; set; }
    public DateOnly? FechaActivacion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public string UsuarioCreacion { get; set; } = string.Empty;
    public string? UsuarioModificacion { get; set; }
}

public class CrearClienteDto
{
    public string Ruc { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string RazonSocial { get; set; } = string.Empty;
    public string NombreComercial { get; set; } = string.Empty;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? CorreoContacto { get; set; }
    public string ServidorSql { get; set; } = string.Empty;
    public string BdNombre { get; set; } = string.Empty;
    public string? BdUsuario { get; set; }
    public string? BdPasswordCifrada { get; set; }
    public int IdEstado { get; set; }
    public DateOnly? FechaActivacion { get; set; }
    public string UsuarioCreacion { get; set; } = string.Empty;
}

public class ActualizarClienteDto
{
    public string Ruc { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string RazonSocial { get; set; } = string.Empty;
    public string NombreComercial { get; set; } = string.Empty;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? CorreoContacto { get; set; }
    public string ServidorSql { get; set; } = string.Empty;
    public string BdNombre { get; set; } = string.Empty;
    public string? BdUsuario { get; set; }
    public string? BdPasswordCifrada { get; set; }
    public int IdEstado { get; set; }
    public DateOnly? FechaActivacion { get; set; }
    public string UsuarioModificacion { get; set; } = string.Empty;
}
