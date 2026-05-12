using ColegioMaster.Infrastructure;
using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios;
using ColegioMaster.Infrastructure.Repositorios.Modelos;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ColegioMaster.Infrastructure.IntegrationTests;

public class RepositorioCrudIntegrationTests
{
    [Fact]
    public async Task CrudCompleto_EstadoCliente_DebePersistirEnBaseDatos()
    {
        await using var conexion = new SqliteConnection("DataSource=:memory:");
        await conexion.OpenAsync();

        var opciones = new DbContextOptionsBuilder<ColegioDbContext>()
            .UseSqlite(conexion)
            .Options;

        await using var dbContext = new ColegioDbContext(opciones);
        await dbContext.Database.EnsureCreatedAsync();

        var repositorio = new RepositorioCrud<EstadoCliente>(dbContext);

        var creado = await repositorio.CrearAsync(new EstadoCliente
        {
            Id = 99,
            Codigo = "TEST",
            Descripcion = "Estado test"
        });

        Assert.Equal(99, creado.Id);

        var obtenido = await repositorio.ObtenerPorIdAsync(99);
        Assert.NotNull(obtenido);
        Assert.Equal("TEST", obtenido!.Codigo);

        obtenido.Descripcion = "Actualizado";
        await repositorio.ActualizarAsync(obtenido);

        var actualizado = await repositorio.ObtenerPorIdAsync(99);
        Assert.NotNull(actualizado);
        Assert.Equal("Actualizado", actualizado!.Descripcion);

        var eliminado = await repositorio.EliminarAsync(99);
        Assert.True(eliminado);

        var noExiste = await repositorio.ObtenerPorIdAsync(99);
        Assert.Null(noExiste);
    }

    [Fact]
    public async Task ListarAsync_DebeAplicarFiltrosDinamicosYPaginacion()
    {
        await using var conexion = new SqliteConnection("DataSource=:memory:");
        await conexion.OpenAsync();

        var opciones = new DbContextOptionsBuilder<ColegioDbContext>()
            .UseSqlite(conexion)
            .Options;

        await using var dbContext = new ColegioDbContext(opciones);
        await dbContext.Database.EnsureCreatedAsync();

        var repositorio = new RepositorioCrud<Plan>(dbContext);

        await repositorio.CrearAsync(new Plan { Codigo = "BAS", Nombre = "Basico", PrecioMensual = 10, PrecioAnual = 100, UsuarioCreacion = "SYS", FechaCreacion = DateTime.UtcNow });
        await repositorio.CrearAsync(new Plan { Codigo = "PRO", Nombre = "Profesional", PrecioMensual = 20, PrecioAnual = 200, UsuarioCreacion = "SYS", FechaCreacion = DateTime.UtcNow });
        await repositorio.CrearAsync(new Plan { Codigo = "PREM", Nombre = "Premium", PrecioMensual = 30, PrecioAnual = 300, UsuarioCreacion = "SYS", FechaCreacion = DateTime.UtcNow });

        var resultado = await repositorio.ListarAsync(new ConsultaRepositorioRequest
        {
            NumeroPagina = 1,
            TamanioPagina = 1,
            Filters = new List<ItemFilterRequest>
            {
                new() { Campo = "Codigo", Operador = "contains", Valor = "PR" }
            }
        });

        Assert.Equal(2, resultado.TotalRegistros);
        Assert.Single(resultado.Registros);
    }
}
