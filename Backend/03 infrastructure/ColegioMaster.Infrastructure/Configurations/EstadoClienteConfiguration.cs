using ColegioMaster.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMaster.Infrastructure.Configurations;

public class EstadoClienteConfiguration : IEntityTypeConfiguration<EstadoCliente>
{
    public void Configure(EntityTypeBuilder<EstadoCliente> entity)
    {
            entity.HasKey(e => e.Id).HasName("PK_EstadoCliente");

            entity.ToTable("estado_cliente");

            entity.HasIndex(e => e.Codigo, "UQ_EstadoCliente_Codigo").IsUnique();

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

