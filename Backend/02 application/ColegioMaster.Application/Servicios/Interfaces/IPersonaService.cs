using ColegioMaster.Application.Dtos.Crud;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColegioMaster.Application.Servicios.Interfaces
{
    public interface IPersonaService: ICrudServicio <int, PersonaDto, CrearPersonaDto, ActualizarPersonaDto>
    {
    }
}
