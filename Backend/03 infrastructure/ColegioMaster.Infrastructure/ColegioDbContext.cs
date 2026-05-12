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

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteSuscripcion> ClienteSuscripcions { get; set; }

    public virtual DbSet<EstadoCliente> EstadoClientes { get; set; }

    public virtual DbSet<EstadoSuscripcion> EstadoSuscripcions { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<UsuarioPlataforma> UsuarioPlataformas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica automaticamente todas las IEntityTypeConfiguration<T> del ensamblado
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColegioDbContext).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

