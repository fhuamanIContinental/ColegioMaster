# Instrucciones para GitHub Copilot

Estas instrucciones aplican a todo el repositorio.

## Stack del proyecto
- Backend: .NET 10 con Entity Framework Core.
- Frontend: Angular 21 con Signals y Standalone Components.
- Base de datos: SQL Server 2022.

## Reglas generales de codigo (backend)
- Escribir nombres de clases, metodos, variables y comentarios en espanol.
- Usar nombres orientados al negocio (ejemplo: `obtenerMatricula`, `registrarPago`, `calcularPromedio`).
- Mantener convenciones de C#:
  - PascalCase: clases, interfaces, metodos publicos.
  - camelCase: variables locales y parametros.

## Arquitectura por capas
La implementacion del backend debe respetar estas capas:
- `api`
- `application`
- `infrastructure`

Regla de dependencia obligatoria:
- `api` -> `application` -> `infrastructure`
- Nunca en sentido inverso.

## Detalle de capas

### ColegioMaster.Api (capa `api`)
- Responsabilidad: exponer endpoints HTTP, validar formato de entrada y delegar en `application`.
- Regla: solo conoce interfaces de `application`.
- Regla: siempre retorna `GeneralResponse` o `GeneralResponse<T>`.
- Ejemplos de nombres: `MatriculaController`, `PagoController`.

### ColegioMaster.Application (capa `application`)
- Responsabilidad: implementar logica de negocio y reglas de dominio.
- Regla: solo conoce interfaces de `infrastructure`.
- Regla: no contiene SQL embebido.
- Ejemplos de nombres: `MatriculaService`, `PagoService`, `ObtenerHistorialPagos`.

### ColegioMaster.Infrastructure (capa `infrastructure`)
- Responsabilidad: acceso a datos con EF Core y SQL Server 2022.
- Regla: contiene persistencia y mapeo de datos.
- Regla: expone contratos de repositorio para consumo de `application`.
- Ejemplos de nombres: `ColegioDbContext`, `RepositorioEstudiante`.

## Modelos y mapeo
- No crear una capa independiente llamada `modelos` fuera de `api/application/infrastructure`.
- Entidades EF Core (tablas, relaciones, configuraciones Fluent API, `DbContext`): en `infrastructure`.
- DTOs exclusivos de HTTP (request/response de controllers): en `api`.
- DTOs compartidos entre `api` y `application`: en un proyecto compartido de contratos (por ejemplo, `ColegioMaster.Contracts`).
- El proyecto de contratos es transversal y no reemplaza las capas `api/application/infrastructure`.
- Mapeo entre entidad EF y DTO: en `application`.
- Regla: `api` no debe conocer entidades EF Core.
- Regla: `application` no debe depender de tipos definidos en `api`.
- Regla: `infrastructure` no debe devolver DTOs de API; devuelve entidades o modelos de persistencia.

## Reglas de implementacion
- Controllers delgados: sin logica de negocio ni SQL embebido.
- Toda logica de negocio debe vivir en `application` y exponerse por interfaces (ejemplo: `IPagoService`).
- El acceso a datos debe vivir en `infrastructure` y exponerse por interfaces (ejemplo: `IPagoRepository`).
- Usar DTOs para request/response.
- No exponer entidades EF Core directamente desde la API.
- Toda operacion de I/O debe ser asincrona (`async/await`).
- Manejar errores de forma centralizada con middleware de excepciones.
- Registrar logs estructurados por capa con contexto funcional.
- Aplicar paginacion y filtros en endpoints de listado.

## Estandar de respuesta API
Todos los controllers deben retornar `GeneralResponse` o `GeneralResponse<T>`.

```csharp
namespace Model.RequestResponse
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }
        public string TitleMessage { get; set; } = string.Empty;
        public string TextMessage { get; set; } = string.Empty;
        public bool ShowAlert { get; set; }
        public T? Content { get; set; }
    }

    public class GeneralResponse
    {
        public bool Success { get; set; }
        public string TitleMessage { get; set; } = string.Empty;
        public string TextMessage { get; set; } = string.Empty;
        public bool ShowAlert { get; set; }
    }
}
```

## Validaciones
- Validaciones de formato: en `api`.
- Validaciones de negocio: en `application`.

## Pruebas
- Agregar pruebas unitarias para servicios criticos.
- Agregar pruebas de integracion para repositorios y casos criticos.

## Prohibiciones
- No colocar SQL en `controllers` ni en `application`.
- No saltar capas (ejemplo: `api` accediendo directo a `infrastructure`).
- No usar nombres genericos sin contexto de negocio (`data`, `item`, `manager`).
- No usar `snake_case` para clases, metodos o archivos C# (usar PascalCase/camelCase segun corresponda).
