using ColegioMaster.Application.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.RequestResponse;
using ColegioMaster.Application.Dtos.Comun;

namespace ColegioMaster.Api.Controllers;

/// <summary>
/// Controlador base genérico que provee operaciones CRUD estándar: listar, buscar, obtener por ID, crear, actualizar y eliminar.
/// </summary>
/// <typeparam name="TId">Tipo del identificador único de la entidad.</typeparam>
/// <typeparam name="TDto">DTO de lectura de la entidad.</typeparam>
/// <typeparam name="TCrearDto">DTO para la creación de la entidad.</typeparam>
/// <typeparam name="TActualizarDto">DTO para la actualización de la entidad.</typeparam>
[ApiController]
[Authorize]
public abstract class CrudControllerBase<TId, TDto, TCrearDto, TActualizarDto> : ControllerBase
{
    private readonly ICrudServicio<TId, TDto, TCrearDto, TActualizarDto> servicio;

    protected CrudControllerBase(ICrudServicio<TId, TDto, TCrearDto, TActualizarDto> servicio)
    {
        this.servicio = servicio;
    }

    /// <summary>
    /// Retorna una lista paginada de registros.
    /// </summary>
    /// <param name="numeroPagina">Número de página (por defecto 1).</param>
    /// <param name="tamanioPagina">Cantidad de registros por página (por defecto 20).</param>
    /// <returns>Lista paginada de registros.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GeneralResponse<ResultadoPaginadoDto<TDto>>>> Listar(
        [FromQuery] int numeroPagina = 1,
        [FromQuery] int tamanioPagina = 20)
    {
        var consulta = new ConsultaPaginadaDto
        {
            NumeroPagina = numeroPagina,
            TamanioPagina = tamanioPagina
        };

        var resultado = await servicio.ListarAsync(consulta);
        return Ok(new GeneralResponse<ResultadoPaginadoDto<TDto>>
        {
            Success = resultado.Exitoso,
            TitleMessage = resultado.TituloMensaje,
            TextMessage = resultado.TextoMensaje,
            ShowAlert = false,
            Content = resultado.Contenido
        });
    }

    /// <summary>
    /// Busca registros aplicando filtros y paginación.
    /// </summary>
    /// <param name="consulta">Parámetros de búsqueda y paginación.</param>
    /// <returns>Lista paginada de registros que coinciden con los filtros.</returns>
    [HttpPost("buscar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GeneralResponse<ResultadoPaginadoDto<TDto>>>> Buscar([FromBody] ConsultaPaginadaDto consulta)
    {
        var resultado = await servicio.ListarAsync(consulta);
        return Ok(new GeneralResponse<ResultadoPaginadoDto<TDto>>
        {
            Success = resultado.Exitoso,
            TitleMessage = resultado.TituloMensaje,
            TextMessage = resultado.TextoMensaje,
            ShowAlert = false,
            Content = resultado.Contenido
        });
    }

    /// <summary>
    /// Obtiene un registro por su identificador.
    /// </summary>
    /// <param name="id">Identificador único del registro.</param>
    /// <returns>El registro encontrado.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GeneralResponse<TDto>>> ObtenerPorId([FromRoute] TId id)
    {
        var resultado = await servicio.ObtenerPorIdAsync(id);
        if (!resultado.Exitoso)
        {
            return NotFound(new GeneralResponse
            {
                Success = false,
                TitleMessage = resultado.TituloMensaje,
                TextMessage = resultado.TextoMensaje,
                ShowAlert = true
            });
        }

        return Ok(new GeneralResponse<TDto>
        {
            Success = true,
            TitleMessage = resultado.TituloMensaje,
            TextMessage = resultado.TextoMensaje,
            ShowAlert = false,
            Content = resultado.Contenido
        });
    }

    /// <summary>
    /// Crea un nuevo registro.
    /// </summary>
    /// <param name="request">Datos del registro a crear.</param>
    /// <returns>El registro creado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GeneralResponse<TDto>>> Crear([FromBody] TCrearDto request)
    {
        var resultado = await servicio.CrearAsync(request);
        if (!resultado.Exitoso)
        {
            return BadRequest(new GeneralResponse
            {
                Success = false,
                TitleMessage = resultado.TituloMensaje,
                TextMessage = resultado.TextoMensaje,
                ShowAlert = true
            });
        }

        return StatusCode(StatusCodes.Status201Created, new GeneralResponse<TDto>
        {
            Success = true,
            TitleMessage = resultado.TituloMensaje,
            TextMessage = resultado.TextoMensaje,
            ShowAlert = false,
            Content = resultado.Contenido
        });
    }

    /// <summary>
    /// Actualiza un registro existente.
    /// </summary>
    /// <param name="id">Identificador único del registro a actualizar.</param>
    /// <param name="request">Datos actualizados del registro.</param>
    /// <returns>El registro actualizado.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GeneralResponse<TDto>>> Actualizar([FromRoute] TId id, [FromBody] TActualizarDto request)
    {
        var resultado = await servicio.ActualizarAsync(id, request);
        if (!resultado.Exitoso)
        {
            if (resultado.TituloMensaje.Contains("no encontrada", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new GeneralResponse
                {
                    Success = false,
                    TitleMessage = resultado.TituloMensaje,
                    TextMessage = resultado.TextoMensaje,
                    ShowAlert = true
                });
            }

            return BadRequest(new GeneralResponse
            {
                Success = false,
                TitleMessage = resultado.TituloMensaje,
                TextMessage = resultado.TextoMensaje,
                ShowAlert = true
            });
        }

        return Ok(new GeneralResponse<TDto>
        {
            Success = true,
            TitleMessage = resultado.TituloMensaje,
            TextMessage = resultado.TextoMensaje,
            ShowAlert = false,
            Content = resultado.Contenido
        });
    }

    /// <summary>
    /// Elimina un registro por su identificador.
    /// </summary>
    /// <param name="id">Identificador único del registro a eliminar.</param>
    /// <returns>Confirmación de la eliminación.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GeneralResponse>> Eliminar([FromRoute] TId id)
    {
        var resultado = await servicio.EliminarAsync(id);
        if (!resultado.Exitoso)
        {
            return NotFound(new GeneralResponse
            {
                Success = false,
                TitleMessage = resultado.TituloMensaje,
                TextMessage = resultado.TextoMensaje,
                ShowAlert = true
            });
        }

        return Ok(new GeneralResponse
        {
            Success = true,
            TitleMessage = resultado.TituloMensaje,
            TextMessage = resultado.TextoMensaje,
            ShowAlert = false
        });
    }
}
