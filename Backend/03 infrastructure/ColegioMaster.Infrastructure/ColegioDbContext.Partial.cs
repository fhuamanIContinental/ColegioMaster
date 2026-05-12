using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.Infrastructure;

public partial class ColegioDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // La configuración se realiza desde Program.cs mediante inyección de dependencias
            // Solo se ejecuta si no está ya configurado
        }
    }
}
