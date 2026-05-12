using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;

namespace ColegioMaster.Application.Servicios;

public class ClienteSuscripcionService : CrudServicioBase<ClienteSuscripcion, long, ClienteSuscripcionDto, CrearClienteSuscripcionDto, ActualizarClienteSuscripcionDto>, IClienteSuscripcionService
{
    public ClienteSuscripcionService(IRepositorioCrud<ClienteSuscripcion> repositorio)
        : base(repositorio)
    {
    }

    protected override string NombreEntidad => "Suscripcion";

    protected override List<string> ValidarCreacion(CrearClienteSuscripcionDto request)
    {
        var errores = new List<string>();
        if (request.IdCliente <= 0) errores.Add("El campo IdCliente debe ser mayor a cero.");
        if (request.IdPlan <= 0) errores.Add("El campo IdPlan debe ser mayor a cero.");
        if (request.IdEstado <= 0) errores.Add("El campo IdEstado debe ser mayor a cero.");
        if (request.MontoPactado < 0) errores.Add("El campo MontoPactado no puede ser negativo.");
        if (request.FechaFin.HasValue && request.FechaFin.Value < request.FechaInicio)
        {
            errores.Add("FechaFin no puede ser menor a FechaInicio.");
        }

        ValidarRequerido(errores, request.Modalidad, "Modalidad", 20);
        ValidarRequerido(errores, request.UsuarioCreacion, "UsuarioCreacion", 100);
        return errores;
    }

    protected override List<string> ValidarActualizacion(ActualizarClienteSuscripcionDto request)
    {
        var errores = new List<string>();
        if (request.IdCliente <= 0) errores.Add("El campo IdCliente debe ser mayor a cero.");
        if (request.IdPlan <= 0) errores.Add("El campo IdPlan debe ser mayor a cero.");
        if (request.IdEstado <= 0) errores.Add("El campo IdEstado debe ser mayor a cero.");
        if (request.MontoPactado < 0) errores.Add("El campo MontoPactado no puede ser negativo.");
        if (request.FechaFin.HasValue && request.FechaFin.Value < request.FechaInicio)
        {
            errores.Add("FechaFin no puede ser menor a FechaInicio.");
        }

        ValidarRequerido(errores, request.Modalidad, "Modalidad", 20);
        ValidarRequerido(errores, request.UsuarioModificacion, "UsuarioModificacion", 100);
        return errores;
    }

    protected override ClienteSuscripcion MapearEntidadCreacion(CrearClienteSuscripcionDto request)
    {
        return new ClienteSuscripcion
        {
            IdCliente = request.IdCliente,
            IdPlan = request.IdPlan,
            FechaInicio = request.FechaInicio,
            FechaFin = request.FechaFin,
            Modalidad = request.Modalidad.Trim(),
            MontoPactado = request.MontoPactado,
            IdEstado = request.IdEstado,
            FechaCreacion = DateTime.UtcNow,
            UsuarioCreacion = request.UsuarioCreacion.Trim()
        };
    }

    protected override void MapearEntidadActualizacion(ClienteSuscripcion entidad, ActualizarClienteSuscripcionDto request)
    {
        entidad.IdCliente = request.IdCliente;
        entidad.IdPlan = request.IdPlan;
        entidad.FechaInicio = request.FechaInicio;
        entidad.FechaFin = request.FechaFin;
        entidad.Modalidad = request.Modalidad.Trim();
        entidad.MontoPactado = request.MontoPactado;
        entidad.IdEstado = request.IdEstado;
        entidad.FechaModificacion = DateTime.UtcNow;
        entidad.UsuarioModificacion = request.UsuarioModificacion.Trim();
    }

    protected override ClienteSuscripcionDto MapearDto(ClienteSuscripcion entidad)
    {
        return new ClienteSuscripcionDto
        {
            Id = entidad.Id,
            IdCliente = entidad.IdCliente,
            IdPlan = entidad.IdPlan,
            FechaInicio = entidad.FechaInicio,
            FechaFin = entidad.FechaFin,
            Modalidad = entidad.Modalidad,
            MontoPactado = entidad.MontoPactado,
            IdEstado = entidad.IdEstado,
            FechaCreacion = entidad.FechaCreacion,
            FechaModificacion = entidad.FechaModificacion,
            UsuarioCreacion = entidad.UsuarioCreacion,
            UsuarioModificacion = entidad.UsuarioModificacion
        };
    }
}
