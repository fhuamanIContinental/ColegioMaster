using ColegioMaster.Application.Dtos.Crud;

namespace ColegioMaster.Application.Servicios.Interfaces;

public interface IClienteService : ICrudServicio<int, ClienteDto, CrearClienteDto, ActualizarClienteDto>
{
}
