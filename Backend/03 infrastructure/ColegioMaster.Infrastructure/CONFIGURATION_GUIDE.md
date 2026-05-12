# Automatización de Entity Framework Core Scaffold

## 📋 Descripción

Este proyecto utiliza una arquitectura limpia con **Entity Type Configuration Classes** que separan la configuración de mapeo del `DbContext`.

La automatización permite que cada vez que ejecutes scaffold desde la BD, se actualice automáticamente:
1. ✅ Modelos (entidades) en `/Models`
2. ✅ Configuraciones en `/Configurations`
3. ✅ `DbContext` limpio y con `ApplyConfigurationsFromAssembly`

---

## 🚀 Cómo usar

### Opción 1: Script automatizado (Recomendado)

```powershell
# Cwd: Raíz del proyecto
.\scaffold-db.ps1
```

**Qué hace:**
- Ejecuta el scaffold de EF Core
- Limpia automáticamente el `DbContext` (remueve contraseña hardcodeada)
- Actualiza `OnModelCreating` para usar `ApplyConfigurationsFromAssembly`
- Compila el proyecto

### Opción 2: Manual

```powershell
# Desde: 01 api\ColegioMaster.Api

$conn = 'Data Source=localhost;Initial Catalog=master_colegio;Persist Security Info=True;User ID=sa;Password=P@ssw0rd123!;Trust Server Certificate=True'

dotnet ef dbcontext scaffold $conn Microsoft.EntityFrameworkCore.SqlServer `
  -o "..\..\03 infrastructure\ColegioMaster.Infrastructure\Models" `
  --context-dir "..\..\03 infrastructure\ColegioMaster.Infrastructure" `
  --context ColegioDbContext `
  --project "..\..\03 infrastructure\ColegioMaster.Infrastructure" `
  --force
```

---

## 📁 Estructura de Configuraciones

```
Infrastructure/
├── Models/
│   ├── Cliente.cs
│   ├── Plan.cs
│   ├── ClienteSuscripcion.cs
│   └── UsuarioPlataforma.cs
│
├── Configurations/
│   ├── ClienteConfiguration.cs
│   ├── PlanConfiguration.cs
│   ├── ClienteSuscripcionConfiguration.cs
│   └── UsuarioPlataformaConfiguration.cs
│
└── ColegioDbContext.cs
```

---

## 🔧 Estructura de Configuration Class

```csharp
using ColegioMaster.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMaster.Infrastructure.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_Cliente");
        entity.ToTable("cliente");
        entity.HasIndex(e => e.Codigo, "UQ_Cliente_Codigo").IsUnique();
        
        // ... más configuraciones
    }
}
```

---

## 🎯 Ventajas

| Aspecto | Beneficio |
|--------|-----------|
| **Separación de responsabilidades** | Cada entidad tiene su propia configuración |
| **DbContext limpio** | Solo 3-4 líneas en `OnModelCreating` |
| **Mantenibilidad** | Fácil encontrar y editar configuraciones |
| **Escalabilidad** | Agregar nuevas entidades es sencillo |
| **Seguridad** | La contraseña se configura desde `appsettings.json` |
| **Automatización** | Reutilizable cada vez que mapees nuevas tablas |

---

## 📝 Cuando agregar nuevas entidades

### Paso 1: Agregar tabla en SQL Server

```sql
CREATE TABLE nueva_tabla (
    id INT PRIMARY KEY,
    nombre NVARCHAR(100)
);
```

### Paso 2: Ejecutar scaffold

```powershell
.\scaffold-db.ps1
```

### Paso 3: Crear Configuration Class

```csharp
// Infrastructure/Configurations/NuevaTablaConfiguration.cs
public class NuevaTablaConfiguration : IEntityTypeConfiguration<NuevaTabla>
{
    public void Configure(EntityTypeBuilder<NuevaTabla> entity)
    {
        // ... configuración
    }
}
```

El DbContext automáticamente detectará y aplicará la configuración.

---

## ⚙️ Configuración de appsettings

La cadena de conexión se configura desde `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "ColegioConnection": "Data Source=localhost;Initial Catalog=master_colegio;Persist Security Info=True;User ID=sa;Password=P@ssw0rd123!;Trust Server Certificate=True"
  }
}
```

En `Program.cs`:
```csharp
var connectionString = builder.Configuration.GetConnectionString("ColegioConnection");
builder.Services.AddDbContext<ColegioDbContext>(options =>
    options.UseSqlServer(connectionString));
```

---

## 🔐 Seguridad

✅ La contraseña está en `appsettings.json` (no en el código)
✅ `appsettings.json` debe estar en `.gitignore`
✅ El DbContext no contiene strings hardcodeados
✅ Usar `appsettings.Development.json` para desarrollo local

---

## 📚 Referencias

- [Entity Type Configuration Classes](https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties)
- [Reverse Engineering](https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding)
- [ApplyConfigurationsFromAssembly](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.modelbuilder.applyconfigurationsfromassembly)
