namespace ColegioMaster.Application.Dtos.Crud;

public class PlanDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioMensual { get; set; }
    public decimal PrecioAnual { get; set; }
    public int? MaxEstudiante { get; set; }
    public int? MaxUsuario { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public string UsuarioCreacion { get; set; } = string.Empty;
    public string? UsuarioModificacion { get; set; }
}

public class CrearPlanDto
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioMensual { get; set; }
    public decimal PrecioAnual { get; set; }
    public int? MaxEstudiante { get; set; }
    public int? MaxUsuario { get; set; }
    public bool Estado { get; set; } = true;
    public string UsuarioCreacion { get; set; } = string.Empty;
}

public class ActualizarPlanDto
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioMensual { get; set; }
    public decimal PrecioAnual { get; set; }
    public int? MaxEstudiante { get; set; }
    public int? MaxUsuario { get; set; }
    public bool Estado { get; set; }
    public string UsuarioModificacion { get; set; } = string.Empty;
}
