using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ColegioMaster.Api.Controllers;

/// <summary>
/// Controlador para la gestión de suscripciones de clientes. Expone las operaciones CRUD sobre la entidad ClienteSuscripcion.
/// </summary>
[SwaggerTag("Gestión de suscripciones de clientes: listado, búsqueda, creación, actualización y eliminación.")]
[Route("api/[controller]")]
public class ClienteSuscripcionesController : CrudControllerBase<long, ClienteSuscripcionDto, CrearClienteSuscripcionDto, ActualizarClienteSuscripcionDto>
{
    public ClienteSuscripcionesController(IClienteSuscripcionService servicio)
        : base(servicio)
    {
    }
}
