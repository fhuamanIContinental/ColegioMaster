using ColegioMaster.Application.Dtos.Comun;
using ColegioMaster.Application.Dtos.Crud;
using ColegioMaster.Application.Servicios;
using ColegioMaster.Infrastructure.Models;
using ColegioMaster.Infrastructure.Repositorios.Interfaces;
using ColegioMaster.Infrastructure.Repositorios.Modelos;
using Xunit;

namespace ColegioMaster.Application.Tests;

public class PlanServiceTests
{
    [Fact]
    public async Task CrearAsync_DebeRetornarError_CuandoDatosInvalidos()
    {
        var repositorio = new RepositorioCrudFalso<Plan>();
        var servicio = new PlanService(repositorio);

        var request = new CrearPlanDto
        {
            Codigo = string.Empty,
            Nombre = string.Empty,
            PrecioMensual = -1,
            PrecioAnual = -1,
            UsuarioCreacion = string.Empty
        };

        var resultado = await servicio.CrearAsync(request);

        Assert.False(resultado.Exitoso);
        Assert.Equal("Solicitud invalida", resultado.TituloMensaje);
        Assert.Contains("Codigo", resultado.TextoMensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ListarAsync_DebeAplicarPaginacionYFiltro()
    {
        var repositorio = new RepositorioCrudFalso<Plan>();
        await repositorio.CrearAsync(new Plan { Id = 1, Codigo = "BAS", Nombre = "Basico", PrecioMensual = 10, PrecioAnual = 100, UsuarioCreacion = "SYS", FechaCreacion = DateTime.UtcNow });
        await repositorio.CrearAsync(new Plan { Id = 2, Codigo = "PRO", Nombre = "Pro", PrecioMensual = 20, PrecioAnual = 200, UsuarioCreacion = "SYS", FechaCreacion = DateTime.UtcNow });
        await repositorio.CrearAsync(new Plan { Id = 3, Codigo = "PREM", Nombre = "Premium", PrecioMensual = 30, PrecioAnual = 300, UsuarioCreacion = "SYS", FechaCreacion = DateTime.UtcNow });

        var servicio = new PlanService(repositorio);

        var resultado = await servicio.ListarAsync(new ConsultaPaginadaDto
        {
            NumeroPagina = 1,
            TamanioPagina = 1,
            Filters = new List<ItemFilterRequest>
            {
                new() { Campo = "Codigo", Operador = "contains", Valor = "PRO" }
            }
        });

        Assert.True(resultado.Exitoso);
        Assert.NotNull(resultado.Contenido);
        Assert.Equal(1, resultado.Contenido.TotalRegistros);
        Assert.Single(resultado.Contenido.Registros);
        Assert.Equal("PRO", resultado.Contenido.Registros[0].Codigo);
    }

    private sealed class RepositorioCrudFalso<TEntidad> : IRepositorioCrud<TEntidad>
        where TEntidad : class
    {
        private readonly List<TEntidad> registros = new();

        public Task<TEntidad> CrearAsync(TEntidad entidad)
        {
            registros.Add(entidad);
            return Task.FromResult(entidad);
        }

        public Task<bool> EliminarAsync(object id)
        {
            var entidad = registros.FirstOrDefault(x => ObtenerId(x)?.Equals(id) == true);
            if (entidad is null)
            {
                return Task.FromResult(false);
            }

            registros.Remove(entidad);
            return Task.FromResult(true);
        }

        public Task<ResultadoPaginadoRepositorio<TEntidad>> ListarAsync(ConsultaRepositorioRequest? consulta = null)
        {
            consulta ??= new ConsultaRepositorioRequest();

            IEnumerable<TEntidad> query = registros;
            foreach (var filtro in consulta.Filters)
            {
                query = query.Where(x =>
                {
                    var propiedad = typeof(TEntidad).GetProperty(filtro.Campo);
                    var valor = propiedad?.GetValue(x)?.ToString() ?? string.Empty;
                    return valor.Contains(filtro.Valor ?? string.Empty, StringComparison.OrdinalIgnoreCase);
                });
            }

            var filtrados = query.ToList();
            var paginados = filtrados
                .Skip((consulta.NumeroPagina - 1) * consulta.TamanioPagina)
                .Take(consulta.TamanioPagina)
                .ToList();

            return Task.FromResult(new ResultadoPaginadoRepositorio<TEntidad>
            {
                TotalRegistros = filtrados.Count,
                Registros = paginados
            });
        }

        public Task<TEntidad?> ObtenerPorIdAsync(object id)
        {
            return Task.FromResult(registros.FirstOrDefault(x => ObtenerId(x)?.Equals(id) == true));
        }

        public Task ActualizarAsync(TEntidad entidad)
        {
            return Task.CompletedTask;
        }

        private static object? ObtenerId(TEntidad entidad)
        {
            return typeof(TEntidad).GetProperty("Id")?.GetValue(entidad);
        }
    }
}
