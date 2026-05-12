using ColegioMaster.Application.Dtos.Crud;

namespace ColegioMaster.Application.Servicios.Interfaces;

public interface IEstadoSuscripcionService : ICrudServicio<int, EstadoSuscripcionDto, CrearEstadoSuscripcionDto, ActualizarEstadoSuscripcionDto>
{
}
