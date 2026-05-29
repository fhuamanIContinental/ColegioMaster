using ColegioMaster.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMaster.Infrastructure.Configurations;

public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
{
    public void Configure(EntityTypeBuilder<Mascota> entity)
    {
            entity.HasKey(e => e.Id).HasName("PK__Mascota__3213E83FFCB1DFC6");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoriaMascota)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoria_mascota");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Mascota_FechaCreacion")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasPrecision(0)
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Raza)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("raza");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_modificacion");
        
    }
}

