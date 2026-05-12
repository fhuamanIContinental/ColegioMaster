using ColegioMaster.Application.Dtos.Auth;

namespace ColegioMaster.Application.Servicios.Interfaces;

public interface IAutenticacionService
{
    Task<ResultadoAutenticacionDto> IniciarSesionAsync(string nombreUsuario, string claveTextoPlano);
}
