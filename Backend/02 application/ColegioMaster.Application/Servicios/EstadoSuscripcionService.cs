using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;

namespace ColegioMaster.Application.Servicios;

public class EstadoSuscripcionService : CrudServicioBase<EstadoSuscripcion, int, EstadoSuscripcionDto, CrearEstadoSuscripcionDto, ActualizarEstadoSuscripcionDto>, IEstadoSuscripcionService
{
    public EstadoSuscripcionService(IRepositorioCrud<EstadoSuscripcion> repositorio)
        : base(repositorio)
    {
    }

    protected override string NombreEntidad => "EstadoSuscripcion";

    protected override List<string> ValidarCreacion(CrearEstadoSuscripcionDto request)
    {
        var errores = new List<string>();
        if (request.Id <= 0) errores.Add("El campo Id debe ser mayor a cero.");
        ValidarRequerido(errores, request.Codigo, "Codigo", 30);
        ValidarRequerido(errores, request.Descripcion, "Descripcion", 100);
        return errores;
    }

    protected override List<string> ValidarActualizacion(ActualizarEstadoSuscripcionDto request)
    {
        var errores = new List<string>();
        ValidarRequerido(errores, request.Codigo, "Codigo", 30);
        ValidarRequerido(errores, request.Descripcion, "Descripcion", 100);
        return errores;
    }

    protected override EstadoSuscripcion MapearEntidadCreacion(CrearEstadoSuscripcionDto request)
    {
        return new EstadoSuscripcion
        {
            Id = request.Id,
            Codigo = request.Codigo.Trim(),
            Descripcion = request.Descripcion.Trim()
        };
    }

    protected override void MapearEntidadActualizacion(EstadoSuscripcion entidad, ActualizarEstadoSuscripcionDto request)
    {
        entidad.Codigo = request.Codigo.Trim();
        entidad.Descripcion = request.Descripcion.Trim();
    }

    protected override EstadoSuscripcionDto MapearDto(EstadoSuscripcion entidad)
    {
        return new EstadoSuscripcionDto
        {
            Id = entidad.Id,
            Codigo = entidad.Codigo,
            Descripcion = entidad.Descripcion
        };
    }
}
