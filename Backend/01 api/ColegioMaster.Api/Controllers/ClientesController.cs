using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ColegioMaster.Api.Controllers;

/// <summary>
/// Controlador para la gestión de clientes. Expone las operaciones CRUD sobre la entidad Cliente.
/// </summary>
[SwaggerTag("Gestión de clientes: listado, búsqueda, creación, actualización y eliminación.")]
[Route("api/[controller]")]
public class ClientesController : CrudControllerBase<int, ClienteDto, CrearClienteDto, ActualizarClienteDto>
{
    public ClientesController(IClienteService servicio)
        : base(servicio)
    {
    }
}
