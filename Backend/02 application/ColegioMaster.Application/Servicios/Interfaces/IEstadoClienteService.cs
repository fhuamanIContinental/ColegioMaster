using ColegioMaster.Application.Dtos.Crud;

namespace ColegioMaster.Application.Servicios.Interfaces;

public interface IEstadoClienteService : ICrudServicio<int, EstadoClienteDto, CrearEstadoClienteDto, ActualizarEstadoClienteDto>
{
}
