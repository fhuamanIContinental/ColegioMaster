using ColegioMaster.Application.Dtos.Comun;

namespace ColegioMaster.Application.Servicios.Interfaces;

public interface ICrudServicio<TId, TDto, in TCrearDto, in TActualizarDto>
{
    Task<ResultadoOperacionDto<ResultadoPaginadoDto<TDto>>> ListarAsync(ConsultaPaginadaDto? consulta = null);
    Task<ResultadoOperacionDto<TDto>> ObtenerPorIdAsync(TId id);
    Task<ResultadoOperacionDto<TDto>> CrearAsync(TCrearDto request);
    Task<ResultadoOperacionDto<TDto>> ActualizarAsync(TId id, TActualizarDto request);
    Task<ResultadoOperacionDto> EliminarAsync(TId id);
}
