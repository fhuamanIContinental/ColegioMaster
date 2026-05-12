using ColegioMaster.Infrastructure.Models;

namespace ColegioMaster.Infrastructure.Repositorios.Interfaces;

public interface IUsuarioPlataformaRepositorio
{
    Task<UsuarioPlataforma?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
    Task ActualizarAsync(UsuarioPlataforma usuario);
}
