using ColegioMaster.Infrastructure.Repositorios.Modelos;

namespace ColegioMaster.Infrastructure.Repositorios.Interfaces;

public interface IRepositorioCrud<TEntidad>
    where TEntidad : class
{
    Task<ResultadoPaginadoRepositorio<TEntidad>> ListarAsync(ConsultaRepositorioRequest? consulta = null);
    Task<TEntidad?> ObtenerPorIdAsync(object id);
    Task<TEntidad> CrearAsync(TEntidad entidad);
    Task ActualizarAsync(TEntidad entidad);
    Task<bool> EliminarAsync(object id);
}
