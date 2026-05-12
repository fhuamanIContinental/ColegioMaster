using ColegioMaster.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMaster.Infrastructure.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> entity)
    {
            entity.HasKey(e => e.Id).HasName("PK_Plan");

            entity.ToTable("plan");

            entity.HasIndex(e => e.Codigo, "UQ_Plan_Codigo").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .HasColumnName("codigo");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true, "DF_Plan_Estado")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Plan_FechaCreacion")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasPrecision(0)
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.MaxEstudiante).HasColumnName("max_estudiante");
            entity.Property(e => e.MaxUsuario).HasColumnName("max_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(120)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioAnual)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("precio_anual");
            entity.Property(e => e.PrecioMensual)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("precio_mensual");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_modificacion");
        
    }
}

