﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class TrackingAndTraceNetCoreContext : DbContext
{
    public TrackingAndTraceNetCoreContext()
    {
    }

    public TrackingAndTraceNetCoreContext(DbContextOptions<TrackingAndTraceNetCoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<EstatusEntrega> EstatusEntregas { get; set; }

    public virtual DbSet<EstatusUnidad> EstatusUnidads { get; set; }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<Repartidor> Repartidors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<UnidadEntrega> UnidadEntregas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-7SN4MG5E; Database= TrackingAndTraceNetCore; TrustServerCertificate=True; Trusted_Connection=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.IdEntrega).HasName("PK__Entrega__C852F553C790B650");

            entity.ToTable("Entrega");

            entity.Property(e => e.FechaEntrega).HasColumnType("date");

            entity.HasOne(d => d.IdEstatusEntregaNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdEstatusEntrega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__IdEstat__22AA2996");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__IdPaque__20C1E124");

            entity.HasOne(d => d.IdRepartidorNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdRepartidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__IdRepar__21B6055D");
        });

        modelBuilder.Entity<EstatusEntrega>(entity =>
        {
            entity.HasKey(e => e.IdEstatus).HasName("PK__EstatusE__B32BA1C743B7001B");

            entity.ToTable("EstatusEntrega");

            entity.Property(e => e.Estatus)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstatusUnidad>(entity =>
        {
            entity.HasKey(e => e.IdEstatus).HasName("PK__EstatusU__B32BA1C751226057");

            entity.ToTable("EstatusUnidad");

            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK__Paquete__DE278F8BF69098A7");

            entity.ToTable("Paquete");

            entity.Property(e => e.Detalle)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.DireccionEntrega)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DireccionOrigen)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaEstimadaEntrega).HasColumnType("date");
            entity.Property(e => e.Peso)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Repartidor>(entity =>
        {
            entity.HasKey(e => e.IdRepartidor).HasName("PK__Repartid__BF0B3B9AFF1A7D16");

            entity.ToTable("Repartidor");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.Fotografia).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUnidadNavigation).WithMany(p => p.Repartidors)
                .HasForeignKey(d => d.IdUnidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Repartido__IdUni__1BFD2C07");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C87437796");

            entity.ToTable("Rol");

            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UnidadEntrega>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PK__UnidadEn__437725E6F5F8D5FA");

            entity.ToTable("UnidadEntrega");

            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroPlaca)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstatusUnidadNavigation).WithMany(p => p.UnidadEntregas)
                .HasForeignKey(d => d.IdEstatusUnidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UnidadEnt__IdEst__1920BF5C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF979E000532");

            entity.ToTable("Usuario");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
