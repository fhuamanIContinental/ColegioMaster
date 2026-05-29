using System;
using System.Collections.Generic;
using ColegioMaster.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.Infrastructure;

public partial class ColegioDbContext : DbContext
{
    public ColegioDbContext()
    {
    }

    public ColegioDbContext(DbContextOptions<ColegioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<ClienteSuscripcion> ClienteSuscripcion { get; set; }

    public virtual DbSet<EstadoCliente> EstadoCliente { get; set; }

    public virtual DbSet<EstadoSuscripcion> EstadoSuscripcion { get; set; }

    public virtual DbSet<Mascota> Mascota { get; set; }

    public virtual DbSet<Persona> Persona { get; set; }

    public virtual DbSet<Plan> Plan { get; set; }

    public virtual DbSet<UsuarioPlataforma> UsuarioPlataforma { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica automaticamente todas las IEntityTypeConfiguration<T> del ensamblado
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColegioDbContext).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

