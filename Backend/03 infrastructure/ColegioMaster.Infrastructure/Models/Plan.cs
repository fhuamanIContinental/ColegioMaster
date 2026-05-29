using System;
using System.Collections.Generic;

namespace ColegioMaster.Infrastructure.Models;

public partial class Plan
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public decimal PrecioMensual { get; set; }

    public decimal PrecioAnual { get; set; }

    public int? MaxEstudiante { get; set; }

    public int? MaxUsuario { get; set; }

    public bool Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<ClienteSuscripcion> ClienteSuscripcion { get; set; } = new List<ClienteSuscripcion>();
}
