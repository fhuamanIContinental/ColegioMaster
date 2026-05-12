using System;
using System.Collections.Generic;

namespace ColegioMaster.Infrastructure.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Ruc { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public string RazonSocial { get; set; } = null!;

    public string NombreComercial { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? CorreoContacto { get; set; }

    public string ServidorSql { get; set; } = null!;

    public string BdNombre { get; set; } = null!;

    public string? BdUsuario { get; set; }

    public string? BdPasswordCifrada { get; set; }

    public int IdEstado { get; set; }

    public DateOnly? FechaActivacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<ClienteSuscripcion> ClienteSuscripcions { get; set; } = new List<ClienteSuscripcion>();

    public virtual EstadoCliente IdEstadoNavigation { get; set; } = null!;
}
