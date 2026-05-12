using ColegioMaster.Infrastructure.Repositorios.Interfaces;
using ColegioMaster.Infrastructure.Repositorios.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace ColegioMaster.Infrastructure.Repositorios;

public class RepositorioCrud<TEntidad> : IRepositorioCrud<TEntidad>
    where TEntidad : class
{
    private readonly ColegioDbContext dbContext;
    private readonly DbSet<TEntidad> entidadSet;

    public RepositorioCrud(ColegioDbContext dbContext)
    {
        this.dbContext = dbContext;
        entidadSet = dbContext.Set<TEntidad>();
    }

    public async Task<ResultadoPaginadoRepositorio<TEntidad>> ListarAsync(ConsultaRepositorioRequest? consulta = null)
    {
        consulta ??= new ConsultaRepositorioRequest();

        var numeroPagina = consulta.NumeroPagina <= 0 ? 1 : consulta.NumeroPagina;
        var tamanioPagina = consulta.TamanioPagina <= 0 ? 20 : consulta.TamanioPagina;
        if (tamanioPagina > 200)
        {
            tamanioPagina = 200;
        }

        IQueryable<TEntidad> query = entidadSet.AsNoTracking();

        foreach (var filtro in consulta.Filters)
        {
            if (string.IsNullOrWhiteSpace(filtro.Campo))
            {
                continue;
            }

            query = AplicarFiltro(query, filtro);
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((numeroPagina - 1) * tamanioPagina)
            .Take(tamanioPagina)
            .ToListAsync();

        return new ResultadoPaginadoRepositorio<TEntidad>
        {
            TotalRegistros = totalRegistros,
            Registros = registros
        };
    }

    public async Task<TEntidad?> ObtenerPorIdAsync(object id)
    {
        return await entidadSet.FindAsync(new[] { id });
    }

    public async Task<TEntidad> CrearAsync(TEntidad entidad)
    {
        await entidadSet.AddAsync(entidad);
        await dbContext.SaveChangesAsync();
        return entidad;
    }

    public async Task ActualizarAsync(TEntidad entidad)
    {
        entidadSet.Update(entidad);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> EliminarAsync(object id)
    {
        var entidad = await ObtenerPorIdAsync(id);
        if (entidad is null)
        {
            return false;
        }

        entidadSet.Remove(entidad);
        await dbContext.SaveChangesAsync();
        return true;
    }

    private static IQueryable<TEntidad> AplicarFiltro(IQueryable<TEntidad> query, ItemFilterRequest filtro)
    {
        var parametro = Expression.Parameter(typeof(TEntidad), "x");
        var propiedad = ObtenerPropiedadPorNombre(typeof(TEntidad), filtro.Campo);
        if (propiedad is null)
        {
            return query;
        }

        var accesoPropiedad = Expression.Property(parametro, propiedad);
        var expresion = ConstruirExpresion(accesoPropiedad, propiedad.PropertyType, filtro);
        if (expresion is null)
        {
            return query;
        }

        var lambda = Expression.Lambda<Func<TEntidad, bool>>(expresion, parametro);
        return query.Where(lambda);
    }

    private static Expression? ConstruirExpresion(MemberExpression accesoPropiedad, Type tipoPropiedad, ItemFilterRequest filtro)
    {
        var tipoReal = Nullable.GetUnderlyingType(tipoPropiedad) ?? tipoPropiedad;
        if (!TryConvertirValor(filtro.Valor, tipoReal, out var valorConvertido))
        {
            return null;
        }

        var constante = Expression.Constant(valorConvertido, tipoReal);
        Expression izquierda = accesoPropiedad;

        if (Nullable.GetUnderlyingType(tipoPropiedad) is not null)
        {
            izquierda = Expression.Convert(accesoPropiedad, tipoReal);
        }

        var op = (filtro.Operador ?? "eq").Trim().ToLowerInvariant();
        return op switch
        {
            "eq" => Expression.Equal(izquierda, constante),
            "neq" => Expression.NotEqual(izquierda, constante),
            "gt" => Expression.GreaterThan(izquierda, constante),
            "gte" => Expression.GreaterThanOrEqual(izquierda, constante),
            "lt" => Expression.LessThan(izquierda, constante),
            "lte" => Expression.LessThanOrEqual(izquierda, constante),
            "contains" when tipoReal == typeof(string) => ConstruirContains(izquierda, valorConvertido?.ToString() ?? string.Empty),
            "startswith" when tipoReal == typeof(string) => ConstruirStartsWith(izquierda, valorConvertido?.ToString() ?? string.Empty),
            "endswith" when tipoReal == typeof(string) => ConstruirEndsWith(izquierda, valorConvertido?.ToString() ?? string.Empty),
            _ => Expression.Equal(izquierda, constante)
        };
    }

    private static Expression ConstruirContains(Expression izquierda, string valor)
    {
        var metodo = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;
        return Expression.Call(izquierda, metodo, Expression.Constant(valor));
    }

    private static Expression ConstruirStartsWith(Expression izquierda, string valor)
    {
        var metodo = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) })!;
        return Expression.Call(izquierda, metodo, Expression.Constant(valor));
    }

    private static Expression ConstruirEndsWith(Expression izquierda, string valor)
    {
        var metodo = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) })!;
        return Expression.Call(izquierda, metodo, Expression.Constant(valor));
    }

    private static PropertyInfo? ObtenerPropiedadPorNombre(Type tipoEntidad, string nombreCampo)
    {
        return tipoEntidad
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .FirstOrDefault(p => string.Equals(p.Name, nombreCampo, StringComparison.OrdinalIgnoreCase));
    }

    private static bool TryConvertirValor(string? valor, Type tipoDestino, out object? resultado)
    {
        resultado = null;
        if (valor is null)
        {
            return false;
        }

        if (tipoDestino == typeof(string))
        {
            resultado = valor;
            return true;
        }

        if (tipoDestino == typeof(int) && int.TryParse(valor, out var intVal))
        {
            resultado = intVal;
            return true;
        }

        if (tipoDestino == typeof(long) && long.TryParse(valor, out var longVal))
        {
            resultado = longVal;
            return true;
        }

        if (tipoDestino == typeof(decimal) && decimal.TryParse(valor, out var decVal))
        {
            resultado = decVal;
            return true;
        }

        if (tipoDestino == typeof(double) && double.TryParse(valor, out var dblVal))
        {
            resultado = dblVal;
            return true;
        }

        if (tipoDestino == typeof(bool) && bool.TryParse(valor, out var boolVal))
        {
            resultado = boolVal;
            return true;
        }

        if (tipoDestino == typeof(DateTime) && DateTime.TryParse(valor, out var dateTimeVal))
        {
            resultado = dateTimeVal;
            return true;
        }

        if (tipoDestino == typeof(DateOnly) && DateOnly.TryParse(valor, out var dateOnlyVal))
        {
            resultado = dateOnlyVal;
            return true;
        }

        return false;
    }
}
