using System;
using System.Collections.Generic;

namespace ColegioMaster.Infrastructure.Models;

public partial class ClienteSuscripcion
{
    public long Id { get; set; }

    public int IdCliente { get; set; }

    public int IdPlan { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string Modalidad { get; set; } = null!;

    public decimal MontoPactado { get; set; }

    public int IdEstado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public string? UsuarioModificacion { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual EstadoSuscripcion IdEstadoNavigation { get; set; } = null!;

    public virtual Plan IdPlanNavigation { get; set; } = null!;
}
