using ColegioMaster.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColegioMaster.Infrastructure.Configurations;

public class UsuarioPlataformaConfiguration : IEntityTypeConfiguration<UsuarioPlataforma>
{
    public void Configure(EntityTypeBuilder<UsuarioPlataforma> entity)
    {
            entity.HasKey(e => e.Id).HasName("PK_UsuarioPlataforma");

            entity.ToTable("usuario_plataforma");

            entity.HasIndex(e => e.Correo, "UQ_UsuarioPlataforma_Correo").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(120)
                .HasColumnName("apellidos");
            entity.Property(e => e.BloqueadoHasta)
                .HasPrecision(0)
                .HasColumnName("bloqueado_hasta");
            entity.Property(e => e.ClaveCifrada)
                .HasMaxLength(500)
                .HasColumnName("clave_cifrada");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true, "DF_UsuarioPlataforma_Estado")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_UsuarioPlataforma_FechaCreacion")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasPrecision(0)
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.IntentosFallidos).HasColumnName("intentos_fallidos");
            entity.Property(e => e.Nombres)
                .HasMaxLength(120)
                .HasColumnName("nombres");
            entity.Property(e => e.UltimoAcceso)
                .HasPrecision(0)
                .HasColumnName("ultimo_acceso");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_modificacion");
        
    }
}

