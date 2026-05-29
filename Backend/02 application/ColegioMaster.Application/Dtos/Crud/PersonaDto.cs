using System;
using System.Collections.Generic;
using System.Text;

namespace ColegioMaster.Application.Dtos.Crud
{
    public class PersonaDto
    {
        public int Id { get; set; }

        public string? TipoDocumento { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? Nombres { get; set; }

        public string? Paterno { get; set; }

        public string? Materno { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string? UsuarioModificacion { get; set; }
    }

    public class CrearPersonaDto
    {
        public int Id { get; set; }

        public string? TipoDocumento { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? Nombres { get; set; }

        public string? Paterno { get; set; }

        public string? Materno { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
    }

    public class ActualizarPersonaDto
    {
        public int Id { get; set; }

        public string? TipoDocumento { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? Nombres { get; set; }

        public string? Paterno { get; set; }

        public string? Materno { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}
