using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ColegioMaster.Api.Controllers;

/// <summary>
/// Controlador para la gestión de estados de suscripciones. Expone las operaciones CRUD sobre la entidad EstadoSuscripcion.
/// </summary>
[SwaggerTag("Gestión de estados de suscripciones: listado, búsqueda, creación, actualización y eliminación.")]
[Route("api/[controller]")]
public class EstadoSuscripcionesController : CrudControllerBase<int, EstadoSuscripcionDto, CrearEstadoSuscripcionDto, ActualizarEstadoSuscripcionDto>
{
    public EstadoSuscripcionesController(IEstadoSuscripcionService servicio)
        : base(servicio)
    {
        
        
    }
}
