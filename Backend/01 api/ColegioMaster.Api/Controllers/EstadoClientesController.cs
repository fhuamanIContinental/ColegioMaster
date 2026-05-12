using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ColegioMaster.Api.Controllers;

/// <summary>
/// Controlador para la gestión de estados de clientes. Expone las operaciones CRUD sobre la entidad EstadoCliente.
/// </summary>
[SwaggerTag("Gestión de estados de clientes: listado, búsqueda, creación, actualización y eliminación.")]
[Route("api/[controller]")]
public class EstadoClientesController : CrudControllerBase<int, EstadoClienteDto, CrearEstadoClienteDto, ActualizarEstadoClienteDto>
{
    public EstadoClientesController(IEstadoClienteService servicio)
        : base(servicio)
    {
    }
}
