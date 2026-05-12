using ColegioMaster.Application.Dtos.Crud;

namespace ColegioMaster.Application.Servicios.Interfaces;

public interface IClienteSuscripcionService : ICrudServicio<long, ClienteSuscripcionDto, CrearClienteSuscripcionDto, ActualizarClienteSuscripcionDto>
{
}
