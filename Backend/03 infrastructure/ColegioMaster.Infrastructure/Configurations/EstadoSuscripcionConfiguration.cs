using ColegioMaster.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMaster.Infrastructure.Configurations;

public class EstadoSuscripcionConfiguration : IEntityTypeConfiguration<EstadoSuscripcion>
{
    public void Configure(EntityTypeBuilder<EstadoSuscripcion> entity)
    {
            entity.HasKey(e => e.Id).HasName("PK_EstadoSuscripcion");

            entity.ToTable("estado_suscripcion");

            entity.HasIndex(e => e.Codigo, "UQ_EstadoSuscripcion_Codigo").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
        
    }
}

