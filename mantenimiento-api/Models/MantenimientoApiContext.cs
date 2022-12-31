using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace mantenimiento_api.Models;

public partial class MantenimientoApiContext : DbContext
{
    public MantenimientoApiContext()
    {
    }

    public MantenimientoApiContext(DbContextOptions<MantenimientoApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Observation> Observations { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolsViewsPermission> RolsViewsPermissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<View> Views { get; set; }

    public virtual DbSet<ViewsPermission> ViewsPermissions { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Observation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Observations_Id");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Observations)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Observations_IdUser");

            entity.HasOne(d => d.IdWorkOrderNavigation).WithMany(p => p.Observations)
                .HasForeignKey(d => d.IdWorkOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Observations_IdWorkOrder");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Permissions_Id");

            entity.Property(e => e.Description).HasMaxLength(160);
            entity.Property(e => e.Name).HasMaxLength(60);
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Pictures_Id");

            entity.HasOne(d => d.IdWorkOrderNavigation).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.IdWorkOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_IdWorkOrder");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Rols_Id");

            entity.Property(e => e.Description).HasMaxLength(160);
            entity.Property(e => e.Name).HasMaxLength(60);
        });

        modelBuilder.Entity<RolsViewsPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_RolsPermissions_Id");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolsViewsPermissions)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolsPermissions_IdRol");

            entity.HasOne(d => d.IdViewPermissionNavigation).WithMany(p => p.RolsViewsPermissions)
                .HasForeignKey(d => d.IdViewPermission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolsPermissions_IdViewPermission");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users_Id");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.Name).HasMaxLength(60);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_IdRol");
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Views_Id");

            entity.Property(e => e.Description).HasMaxLength(160);
            entity.Property(e => e.Name).HasMaxLength(60);
        });

        modelBuilder.Entity<ViewsPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ViewsPermissions_Id");

            entity.HasOne(d => d.IdPermissionNavigation).WithMany(p => p.ViewsPermissions)
                .HasForeignKey(d => d.IdPermission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ViewsPermissions_IdPermission");

            entity.HasOne(d => d.IdViewNavigation).WithMany(p => p.ViewsPermissions)
                .HasForeignKey(d => d.IdView)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ViewsPermissions_IdView");
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_WorkOrders_Id");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.FinishDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdUserAsignedNavigation).WithMany(p => p.WorkOrderIdUserAsignedNavigations)
                .HasForeignKey(d => d.IdUserAsigned)
                .HasConstraintName("FK_WorkOrders_IdUserAsigned");

            entity.HasOne(d => d.IdUserCreatorNavigation).WithMany(p => p.WorkOrderIdUserCreatorNavigations)
                .HasForeignKey(d => d.IdUserCreator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkOrders_IdUserCreator");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
