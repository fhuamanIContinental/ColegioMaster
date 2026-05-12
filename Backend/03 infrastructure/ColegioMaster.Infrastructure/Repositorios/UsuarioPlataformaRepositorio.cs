using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.Infrastructure.Repositorios;

public class UsuarioPlataformaRepositorio : IUsuarioPlataformaRepositorio
{
    private readonly ColegioDbContext dbContext;

    public UsuarioPlataformaRepositorio(ColegioDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<UsuarioPlataforma?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
    {
        return await dbContext.UsuarioPlataformas
            .FirstOrDefaultAsync(x => x.Correo == nombreUsuario);
    }

    public async Task ActualizarAsync(UsuarioPlataforma usuario)
    {
        dbContext.UsuarioPlataformas.Update(usuario);
        await dbContext.SaveChangesAsync();
    }
}
