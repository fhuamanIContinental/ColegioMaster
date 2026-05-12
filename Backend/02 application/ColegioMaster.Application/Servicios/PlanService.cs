using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;

namespace ColegioMaster.Application.Servicios;

public class PlanService : CrudServicioBase<Plan, int, PlanDto, CrearPlanDto, ActualizarPlanDto>, IPlanService
{
    public PlanService(IRepositorioCrud<Plan> repositorio)
        : base(repositorio)
    {
    }

    protected override string NombreEntidad => "Plan";

    protected override List<string> ValidarCreacion(CrearPlanDto request)
    {
        var errores = new List<string>();
        ValidarRequerido(errores, request.Codigo, "Codigo", 30);
        ValidarRequerido(errores, request.Nombre, "Nombre", 120);
        ValidarRequerido(errores, request.UsuarioCreacion, "UsuarioCreacion", 100);

        if (request.PrecioMensual < 0) errores.Add("El campo PrecioMensual no puede ser negativo.");
        if (request.PrecioAnual < 0) errores.Add("El campo PrecioAnual no puede ser negativo.");
        if (request.MaxEstudiante.HasValue && request.MaxEstudiante.Value < 0) errores.Add("El campo MaxEstudiante no puede ser negativo.");
        if (request.MaxUsuario.HasValue && request.MaxUsuario.Value < 0) errores.Add("El campo MaxUsuario no puede ser negativo.");

        return errores;
    }

    protected override List<string> ValidarActualizacion(ActualizarPlanDto request)
    {
        var errores = new List<string>();
        ValidarRequerido(errores, request.Codigo, "Codigo", 30);
        ValidarRequerido(errores, request.Nombre, "Nombre", 120);
        ValidarRequerido(errores, request.UsuarioModificacion, "UsuarioModificacion", 100);

        if (request.PrecioMensual < 0) errores.Add("El campo PrecioMensual no puede ser negativo.");
        if (request.PrecioAnual < 0) errores.Add("El campo PrecioAnual no puede ser negativo.");
        if (request.MaxEstudiante.HasValue && request.MaxEstudiante.Value < 0) errores.Add("El campo MaxEstudiante no puede ser negativo.");
        if (request.MaxUsuario.HasValue && request.MaxUsuario.Value < 0) errores.Add("El campo MaxUsuario no puede ser negativo.");

        return errores;
    }

    protected override Plan MapearEntidadCreacion(CrearPlanDto request)
    {
        return new Plan
        {
            Codigo = request.Codigo.Trim(),
            Nombre = request.Nombre.Trim(),
            PrecioMensual = request.PrecioMensual,
            PrecioAnual = request.PrecioAnual,
            MaxEstudiante = request.MaxEstudiante,
            MaxUsuario = request.MaxUsuario,
            Estado = request.Estado,
            FechaCreacion = DateTime.UtcNow,
            UsuarioCreacion = request.UsuarioCreacion.Trim()
        };
    }

    protected override void MapearEntidadActualizacion(Plan entidad, ActualizarPlanDto request)
    {
        entidad.Codigo = request.Codigo.Trim();
        entidad.Nombre = request.Nombre.Trim();
        entidad.PrecioMensual = request.PrecioMensual;
        entidad.PrecioAnual = request.PrecioAnual;
        entidad.MaxEstudiante = request.MaxEstudiante;
        entidad.MaxUsuario = request.MaxUsuario;
        entidad.Estado = request.Estado;
        entidad.FechaModificacion = DateTime.UtcNow;
        entidad.UsuarioModificacion = request.UsuarioModificacion.Trim();
    }

    protected override PlanDto MapearDto(Plan entidad)
    {
        return new PlanDto
        {
            Id = entidad.Id,
            Codigo = entidad.Codigo,
            Nombre = entidad.Nombre,
            PrecioMensual = entidad.PrecioMensual,
            PrecioAnual = entidad.PrecioAnual,
            MaxEstudiante = entidad.MaxEstudiante,
            MaxUsuario = entidad.MaxUsuario,
            Estado = entidad.Estado,
            FechaCreacion = entidad.FechaCreacion,
            FechaModificacion = entidad.FechaModificacion,
            UsuarioCreacion = entidad.UsuarioCreacion,
            UsuarioModificacion = entidad.UsuarioModificacion
        };
    }
}
