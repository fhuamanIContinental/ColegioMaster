using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;

namespace ColegioMaster.Application.Servicios;

public class UsuarioPlataformaService : CrudServicioBase<UsuarioPlataforma, long, UsuarioPlataformaDto, CrearUsuarioPlataformaDto, ActualizarUsuarioPlataformaDto>, IUsuarioPlataformaService
{
    public UsuarioPlataformaService(IRepositorioCrud<UsuarioPlataforma> repositorio)
        : base(repositorio)
    {
    }

    protected override string NombreEntidad => "UsuarioPlataforma";

    protected override List<string> ValidarCreacion(CrearUsuarioPlataformaDto request)
    {
        var errores = new List<string>();
        ValidarRequerido(errores, request.Nombres, "Nombres", 120);
        ValidarRequerido(errores, request.Apellidos, "Apellidos", 120);
        ValidarRequerido(errores, request.Correo, "Correo", 150);
        ValidarRequerido(errores, request.ClaveCifrada, "ClaveCifrada", 500);
        ValidarRequerido(errores, request.UsuarioCreacion, "UsuarioCreacion", 100);
        return errores;
    }

    protected override List<string> ValidarActualizacion(ActualizarUsuarioPlataformaDto request)
    {
        var errores = new List<string>();
        ValidarRequerido(errores, request.Nombres, "Nombres", 120);
        ValidarRequerido(errores, request.Apellidos, "Apellidos", 120);
        ValidarRequerido(errores, request.Correo, "Correo", 150);
        ValidarRequerido(errores, request.ClaveCifrada, "ClaveCifrada", 500);
        ValidarRequerido(errores, request.UsuarioModificacion, "UsuarioModificacion", 100);

        if (request.IntentosFallidos < 0)
        {
            errores.Add("El campo IntentosFallidos no puede ser negativo.");
        }

        return errores;
    }

    protected override UsuarioPlataforma MapearEntidadCreacion(CrearUsuarioPlataformaDto request)
    {
        return new UsuarioPlataforma
        {
            Nombres = request.Nombres.Trim(),
            Apellidos = request.Apellidos.Trim(),
            Correo = request.Correo.Trim(),
            ClaveCifrada = request.ClaveCifrada,
            IntentosFallidos = 0,
            Estado = request.Estado,
            FechaCreacion = DateTime.UtcNow,
            UsuarioCreacion = request.UsuarioCreacion.Trim()
        };
    }

    protected override void MapearEntidadActualizacion(UsuarioPlataforma entidad, ActualizarUsuarioPlataformaDto request)
    {
        entidad.Nombres = request.Nombres.Trim();
        entidad.Apellidos = request.Apellidos.Trim();
        entidad.Correo = request.Correo.Trim();
        entidad.ClaveCifrada = request.ClaveCifrada;
        entidad.IntentosFallidos = request.IntentosFallidos;
        entidad.BloqueadoHasta = request.BloqueadoHasta;
        entidad.UltimoAcceso = request.UltimoAcceso;
        entidad.Estado = request.Estado;
        entidad.FechaModificacion = DateTime.UtcNow;
        entidad.UsuarioModificacion = request.UsuarioModificacion.Trim();
    }

    protected override UsuarioPlataformaDto MapearDto(UsuarioPlataforma entidad)
    {
        return new UsuarioPlataformaDto
        {
            Id = entidad.Id,
            Nombres = entidad.Nombres,
            Apellidos = entidad.Apellidos,
            Correo = entidad.Correo,
            ClaveCifrada = entidad.ClaveCifrada,
            IntentosFallidos = entidad.IntentosFallidos,
            BloqueadoHasta = entidad.BloqueadoHasta,
            UltimoAcceso = entidad.UltimoAcceso,
            Estado = entidad.Estado,
            FechaCreacion = entidad.FechaCreacion,
            FechaModificacion = entidad.FechaModificacion,
            UsuarioCreacion = entidad.UsuarioCreacion,
            UsuarioModificacion = entidad.UsuarioModificacion
        };
    }
}
