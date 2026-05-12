using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;

namespace ColegioMaster.Application.Servicios;

public class EstadoClienteService : CrudServicioBase<EstadoCliente, int, EstadoClienteDto, CrearEstadoClienteDto, ActualizarEstadoClienteDto>, IEstadoClienteService
{
    public EstadoClienteService(IRepositorioCrud<EstadoCliente> repositorio)
        : base(repositorio)
    {
    }

    protected override string NombreEntidad => "EstadoCliente";

    protected override List<string> ValidarCreacion(CrearEstadoClienteDto request)
    {
        var errores = new List<string>();
        if (request.Id <= 0) errores.Add("El campo Id debe ser mayor a cero.");
        ValidarRequerido(errores, request.Codigo, "Codigo", 30);
        ValidarRequerido(errores, request.Descripcion, "Descripcion", 100);
        return errores;
    }

    protected override List<string> ValidarActualizacion(ActualizarEstadoClienteDto request)
    {
        var errores = new List<string>();
        ValidarRequerido(errores, request.Codigo, "Codigo", 30);
        ValidarRequerido(errores, request.Descripcion, "Descripcion", 100);
        return errores;
    }

    protected override EstadoCliente MapearEntidadCreacion(CrearEstadoClienteDto request)
    {
        return new EstadoCliente
        {
            Id = request.Id,
            Codigo = request.Codigo.Trim(),
            Descripcion = request.Descripcion.Trim()
        };
    }

    protected override void MapearEntidadActualizacion(EstadoCliente entidad, ActualizarEstadoClienteDto request)
    {
        entidad.Codigo = request.Codigo.Trim();
        entidad.Descripcion = request.Descripcion.Trim();
    }

    protected override EstadoClienteDto MapearDto(EstadoCliente entidad)
    {
        return new EstadoClienteDto
        {
            Id = entidad.Id,
            Codigo = entidad.Codigo,
            Descripcion = entidad.Descripcion
        };
    }
}
