using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ColegioMaster.Api.Controllers;

/// <summary>
/// Controlador para la gestión de planes. Expone las operaciones CRUD sobre la entidad Plan.
/// </summary>
[SwaggerTag("Gestión de planes de suscripción: listado, búsqueda, creación, actualización y eliminación.")]
[Route("api/[controller]")]
public class PlanesController : CrudControllerBase<int, PlanDto, CrearPlanDto, ActualizarPlanDto>
{
    public PlanesController(IPlanService servicio)
        : base(servicio)
    {
    }
}
