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

            entity.HasIndex(e => e.BdNombre, "UQ_Cliente_BdNombre").IsUnique();

            entity.HasIndex(e => e.Codigo, "UQ_Cliente_Codigo").IsUnique();

            entity.HasIndex(e => e.Ruc, "UQ_Cliente_Ruc").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BdNombre)
                .HasMaxLength(120)
                .HasColumnName("bd_nombre");
            entity.Property(e => e.BdPasswordCifrada)
                .HasMaxLength(400)
                .HasColumnName("bd_password_cifrada");
            entity.Property(e => e.BdUsuario)
                .HasMaxLength(120)
                .HasColumnName("bd_usuario");
            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .HasColumnName("codigo");
            entity.Property(e => e.CorreoContacto)
                .HasMaxLength(120)
                .HasColumnName("correo_contacto");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaActivacion).HasColumnName("fecha_activacion");
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Cliente_FechaCreacion")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasPrecision(0)
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.IdEstado)
                .HasDefaultValue(1, "DF_Cliente_Estado")
                .HasColumnName("id_estado");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(200)
                .HasColumnName("nombre_comercial");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .HasColumnName("razon_social");
            entity.Property(e => e.Ruc)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ruc");
            entity.Property(e => e.ServidorSql)
                .HasMaxLength(120)
                .HasColumnName("servidor_sql");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .HasColumnName("telefono");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .HasColumnName("usuario_modificacion");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Cliente)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_EstadoCliente");
        
    }
}

