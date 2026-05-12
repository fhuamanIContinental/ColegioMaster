using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ColegioMaster.Api.Controllers;

/// <summary>
/// Controlador para la gestión de usuarios de plataforma. Expone las operaciones CRUD sobre la entidad UsuarioPlataforma.
/// </summary>
[SwaggerTag("Gestión de usuarios de la plataforma: listado, búsqueda, creación, actualización y eliminación.")]
[Route("api/[controller]")]
public class UsuariosPlataformaController : CrudControllerBase<long, UsuarioPlataformaDto, CrearUsuarioPlataformaDto, ActualizarUsuarioPlataformaDto>
{
    public UsuariosPlataformaController(IUsuarioPlataformaService servicio)
        : base(servicio)
    {
    }
}
