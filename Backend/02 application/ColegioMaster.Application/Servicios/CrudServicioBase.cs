using ColegioMaster.Application.Dtos.Comun;
using ColegioMaster.Application.Servicios.Interfaces;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;
using ColegioMaster.Infrastructure.Repositorios.Modelos;

namespace ColegioMaster.Application.Servicios;

public abstract class CrudServicioBase<TEntidad, TId, TDto, TCrearDto, TActualizarDto>
    : ICrudServicio<TId, TDto, TCrearDto, TActualizarDto>
    where TEntidad : class
{
    private readonly IRepositorioCrud<TEntidad> repositorio;

    protected CrudServicioBase(IRepositorioCrud<TEntidad> repositorio)
    {
        this.repositorio = repositorio;
    }

    protected abstract string NombreEntidad { get; }
    protected abstract List<string> ValidarCreacion(TCrearDto request);
    protected abstract List<string> ValidarActualizacion(TActualizarDto request);
    protected abstract TEntidad MapearEntidadCreacion(TCrearDto request);
    protected abstract void MapearEntidadActualizacion(TEntidad entidad, TActualizarDto request);
    protected abstract TDto MapearDto(TEntidad entidad);

    public async Task<ResultadoOperacionDto<ResultadoPaginadoDto<TDto>>> ListarAsync(ConsultaPaginadaDto? consulta = null)
    {
        consulta ??= new ConsultaPaginadaDto();
        var consultaRepositorio = new ConsultaRepositorioRequest
        {
            NumeroPagina = consulta.NumeroPagina,
            TamanioPagina = consulta.TamanioPagina,
            Filters = consulta.Filters
        };

        var respuestaRepositorio = await repositorio.ListarAsync(consultaRepositorio);

        var numeroPagina = consultaRepositorio.NumeroPagina <= 0 ? 1 : consultaRepositorio.NumeroPagina;
        var tamanioPagina = consultaRepositorio.TamanioPagina <= 0 ? 20 : consultaRepositorio.TamanioPagina;

        var totalPaginas = respuestaRepositorio.TotalRegistros == 0
            ? 0
            : (int)Math.Ceiling(respuestaRepositorio.TotalRegistros / (double)tamanioPagina);

        var registros = respuestaRepositorio.Registros
            .Select(MapearDto)
            .ToList();

        var resultadoPaginado = new ResultadoPaginadoDto<TDto>
        {
            NumeroPagina = numeroPagina,
            TamanioPagina = tamanioPagina,
            TotalRegistros = respuestaRepositorio.TotalRegistros,
            TotalPaginas = totalPaginas,
            Registros = registros
        };

        return new ResultadoOperacionDto<ResultadoPaginadoDto<TDto>>
        {
            Exitoso = true,
            TituloMensaje = $"Listado de {NombreEntidad}",
            TextoMensaje = "Operacion completada correctamente.",
            Contenido = resultadoPaginado
        };
    }

    public async Task<ResultadoOperacionDto<TDto>> ObtenerPorIdAsync(TId id)
    {
        var entidad = await repositorio.ObtenerPorIdAsync(id!);
        if (entidad is null)
        {
            return ConstruirRespuestaNoEncontrado<TDto>();
        }

        return new ResultadoOperacionDto<TDto>
        {
            Exitoso = true,
            TituloMensaje = $"Detalle de {NombreEntidad}",
            TextoMensaje = "Registro encontrado.",
            Contenido = MapearDto(entidad)
        };
    }

    public async Task<ResultadoOperacionDto<TDto>> CrearAsync(TCrearDto request)
    {
        var errores = ValidarCreacion(request);
        if (errores.Count > 0)
        {
            return ConstruirRespuestaInvalida<TDto>(errores);
        }

        var entidad = MapearEntidadCreacion(request);
        var entidadCreada = await repositorio.CrearAsync(entidad);

        return new ResultadoOperacionDto<TDto>
        {
            Exitoso = true,
            TituloMensaje = $"{NombreEntidad} creada",
            TextoMensaje = "Registro creado correctamente.",
            Contenido = MapearDto(entidadCreada)
        };
    }

    public async Task<ResultadoOperacionDto<TDto>> ActualizarAsync(TId id, TActualizarDto request)
    {
        var errores = ValidarActualizacion(request);
        if (errores.Count > 0)
        {
            return ConstruirRespuestaInvalida<TDto>(errores);
        }

        var entidad = await repositorio.ObtenerPorIdAsync(id!);
        if (entidad is null)
        {
            return ConstruirRespuestaNoEncontrado<TDto>();
        }

        MapearEntidadActualizacion(entidad, request);
        await repositorio.ActualizarAsync(entidad);

        return new ResultadoOperacionDto<TDto>
        {
            Exitoso = true,
            TituloMensaje = $"{NombreEntidad} actualizada",
            TextoMensaje = "Registro actualizado correctamente.",
            Contenido = MapearDto(entidad)
        };
    }

    public async Task<ResultadoOperacionDto> EliminarAsync(TId id)
    {
        var eliminado = await repositorio.EliminarAsync(id!);
        if (!eliminado)
        {
            return new ResultadoOperacionDto
            {
                Exitoso = false,
                TituloMensaje = $"{NombreEntidad} no encontrada",
                TextoMensaje = "No existe un registro con el identificador enviado."
            };
        }

        return new ResultadoOperacionDto
        {
            Exitoso = true,
            TituloMensaje = $"{NombreEntidad} eliminada",
            TextoMensaje = "Registro eliminado correctamente."
        };
    }

    protected static void ValidarRequerido(List<string> errores, string? valor, string nombreCampo, int? maximo = null)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            errores.Add($"El campo {nombreCampo} es obligatorio.");
            return;
        }

        if (maximo.HasValue && valor.Trim().Length > maximo.Value)
        {
            errores.Add($"El campo {nombreCampo} no puede exceder {maximo.Value} caracteres.");
        }
    }

    private ResultadoOperacionDto<T> ConstruirRespuestaNoEncontrado<T>()
    {
        return new ResultadoOperacionDto<T>
        {
            Exitoso = false,
            TituloMensaje = $"{NombreEntidad} no encontrada",
            TextoMensaje = "No existe un registro con el identificador enviado."
        };
    }

    private ResultadoOperacionDto<T> ConstruirRespuestaInvalida<T>(List<string> errores)
    {
        return new ResultadoOperacionDto<T>
        {
            Exitoso = false,
            TituloMensaje = "Solicitud invalida",
            TextoMensaje = string.Join(" | ", errores)
        };
    }
}
