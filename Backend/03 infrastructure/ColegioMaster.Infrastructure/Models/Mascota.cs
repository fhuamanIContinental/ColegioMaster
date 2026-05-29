using System;
using System.Collections.Generic;

namespace ColegioMaster.Infrastructure.Models;

public partial class Mascota
{
    public int Id { get; set; }

    public string? CategoriaMascota { get; set; }

    public string? Raza { get; set; }

    public int? Edad { get; set; }

    public string? Nombre { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public string? UsuarioModificacion { get; set; }
}
