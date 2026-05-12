using System;
using System.Collections.Generic;

namespace ColegioMaster.Infrastructure.Models;

public partial class UsuarioPlataforma
{
    public long Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string ClaveCifrada { get; set; } = null!;

    public int IntentosFallidos { get; set; }

    public DateTime? BloqueadoHasta { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public bool Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public string? UsuarioModificacion { get; set; }
}
