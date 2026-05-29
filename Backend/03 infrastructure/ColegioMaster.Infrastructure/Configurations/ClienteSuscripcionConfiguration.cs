using ColegioMaster.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMaster.Infrastructure.Configurations;

public class ClienteSuscripcionConfiguration : IEntityTypeConfiguration<ClienteSuscripcion>
{
    public void Configure(EntityTypeBuilder<ClienteSuscripcion> entity)
    {
            entity.HasKey(e => e.Id).HasName("PK_ClienteSuscripcion");

            entity.ToTable("cliente_suscripcion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_ClienteSuscripcion_FechaCreacion")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaModificacion)
                .HasPrecision(0)
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdEstado)
                .HasDefaultValue(1, "DF_ClienteSuscripcion_Estado")
                .HasColumnName("id_estado");
            entity.Property(e => e.IdPlan).HasColumnName("id_plan");
            entity.Property(e => e.Modalidad)
                .HasMaxLength(20)
                .HasColumnName("modalidad");
            entity.Property(e => e.MontoPactado)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("monto_pactado");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_modificacion");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ClienteSuscripcion)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteSuscripcion_Cliente");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.ClienteSuscripcion)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteSuscripcion_EstadoSuscripcion");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.ClienteSuscripcion)
                .HasForeignKey(d => d.IdPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClienteSuscripcion_Plan");
        
    }
}

