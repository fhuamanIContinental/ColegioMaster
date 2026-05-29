using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ColegioMaster.Api.Controllers
{
    /// <summary>
    /// Controlador para la gestión de personas
    /// </summary>
    [SwaggerTag("Gestión de planes de suscripción: listado, búsqueda, creación, actualización y eliminación.")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : CrudControllerBase<int, PersonaDto, CrearPersonaDto, ActualizarPersonaDto>
    {

        //CREANDO NUESTRO CONSTRUCTOR
        public PersonaController(IPersonaService servicio)
        : base(servicio)
        {
        }

    }
}
