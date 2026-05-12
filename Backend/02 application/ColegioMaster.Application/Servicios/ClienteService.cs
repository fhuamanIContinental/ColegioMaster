using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;

namespace ColegioMaster.Application.Servicios;

public class ClienteService : CrudServicioBase<Cliente, int, ClienteDto, CrearClienteDto, ActualizarClienteDto>, IClienteService
{
    public ClienteService(IRepositorioCrud<Cliente> repositorio)
        : base(repositorio)
    {
    }

    protected override string NombreEntidad => "Cliente";

    protected override List<string> ValidarCreacion(CrearClienteDto request)
    {
        var errores = new List<string>();
        ValidarRequerido(errores, request.Ruc, "Ruc", 11);
        ValidarRequerido(errores, request.Codigo, "Codigo", 30);
        ValidarRequerido(errores, request.RazonSocial, "RazonSocial", 200);
        ValidarRequerido(errores, request.NombreComercial, "NombreComercial", 200);
        ValidarRequerido(errores, request.ServidorSql, "ServidorSql", 120);
        ValidarRequerido(errores, request.BdNombre, "BdNombre", 120);
        ValidarRequerido(errores, request.UsuarioCreacion, "UsuarioCreacion", 100);

        if (request.IdEstado <= 0)
        {
            errores.Add("El campo IdEstado debe ser mayor a cero.");
        }

        return errores;
    }

    protected override List<string> ValidarActualizacion(ActualizarClienteDto request)
    {
        var errores = new List<string>();
        ValidarRequerido(errores, request.Ruc, "Ruc", 11);
        ValidarRequerido(errores, request.Codigo, "Codigo", 30);
        ValidarRequerido(errores, request.RazonSocial, "RazonSocial", 200);
        ValidarRequerido(errores, request.NombreComercial, "NombreComercial", 200);
        ValidarRequerido(errores, request.ServidorSql, "ServidorSql", 120);
        ValidarRequerido(errores, request.BdNombre, "BdNombre", 120);
        ValidarRequerido(errores, request.UsuarioModificacion, "UsuarioModificacion", 100);

        if (request.IdEstado <= 0)
        {
            errores.Add("El campo IdEstado debe ser mayor a cero.");
        }

        return errores;
    }

    protected override Cliente MapearEntidadCreacion(CrearClienteDto request)
    {
        return new Cliente
        {
            Ruc = request.Ruc.Trim(),
            Codigo = request.Codigo.Trim(),
            RazonSocial = request.RazonSocial.Trim(),
            NombreComercial = request.NombreComercial.Trim(),
            Direccion = request.Direccion?.Trim(),
            Telefono = request.Telefono?.Trim(),
            CorreoContacto = request.CorreoContacto?.Trim(),
            ServidorSql = request.ServidorSql.Trim(),
            BdNombre = request.BdNombre.Trim(),
            BdUsuario = request.BdUsuario?.Trim(),
            BdPasswordCifrada = request.BdPasswordCifrada,
            IdEstado = request.IdEstado,
            FechaActivacion = request.FechaActivacion,
            FechaCreacion = DateTime.UtcNow,
            UsuarioCreacion = request.UsuarioCreacion.Trim()
        };
    }

    protected override void MapearEntidadActualizacion(Cliente entidad, ActualizarClienteDto request)
    {
        entidad.Ruc = request.Ruc.Trim();
        entidad.Codigo = request.Codigo.Trim();
        entidad.RazonSocial = request.RazonSocial.Trim();
        entidad.NombreComercial = request.NombreComercial.Trim();
        entidad.Direccion = request.Direccion?.Trim();
        entidad.Telefono = request.Telefono?.Trim();
        entidad.CorreoContacto = request.CorreoContacto?.Trim();
        entidad.ServidorSql = request.ServidorSql.Trim();
        entidad.BdNombre = request.BdNombre.Trim();
        entidad.BdUsuario = request.BdUsuario?.Trim();
        entidad.BdPasswordCifrada = request.BdPasswordCifrada;
        entidad.IdEstado = request.IdEstado;
        entidad.FechaActivacion = request.FechaActivacion;
        entidad.FechaModificacion = DateTime.UtcNow;
        entidad.UsuarioModificacion = request.UsuarioModificacion.Trim();
    }

    protected override ClienteDto MapearDto(Cliente entidad)
    {
        return new ClienteDto
        {
            Id = entidad.Id,
            Ruc = entidad.Ruc,
            Codigo = entidad.Codigo,
            RazonSocial = entidad.RazonSocial,
            NombreComercial = entidad.NombreComercial,
            Direccion = entidad.Direccion,
            Telefono = entidad.Telefono,
            CorreoContacto = entidad.CorreoContacto,
            ServidorSql = entidad.ServidorSql,
            BdNombre = entidad.BdNombre,
            BdUsuario = entidad.BdUsuario,
            BdPasswordCifrada = entidad.BdPasswordCifrada,
            IdEstado = entidad.IdEstado,
            FechaActivacion = entidad.FechaActivacion,
            FechaCreacion = entidad.FechaCreacion,
            FechaModificacion = entidad.FechaModificacion,
            UsuarioCreacion = entidad.UsuarioCreacion,
            UsuarioModificacion = entidad.UsuarioModificacion
        };
    }
}
