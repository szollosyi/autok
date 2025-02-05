using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bee_healthy_backend.Models;

public partial class BeeHealthyContext : DbContext
{
    public BeeHealthyContext()
    {
    }

    public BeeHealthyContext(DbContextOptions<BeeHealthyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GyogyszerAdatok> GyogyszerAdatoks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=bee_healthy;USER=root;PASSWORD=;SSL MODE=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GyogyszerAdatok>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("gyogyszer_adatok");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Adagolas).HasMaxLength(64);
            entity.Property(e => e.Emlekezteto).HasColumnType("date");
            entity.Property(e => e.GyogyszerNev)
                .HasMaxLength(64)
                .HasColumnName("Gyogyszer_nev");
            entity.Property(e => e.Kategoria).HasMaxLength(64);
            entity.Property(e => e.KezelesIdotartama)
                .HasMaxLength(64)
                .HasColumnName("Kezeles_idotartama");
            entity.Property(e => e.KezelesiIdopont)
                .HasMaxLength(64)
                .HasColumnName("Kezelesi_idopont");
            entity.Property(e => e.Marka).HasMaxLength(64);
            entity.Property(e => e.Megjegyzes).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.PermissionId, "Jog");

            entity.HasIndex(e => e.LoginNev, "LoginNev").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(64);
            entity.Property(e => e.Hash)
                .HasMaxLength(64)
                .HasColumnName("HASH");
            entity.Property(e => e.LoginNev).HasMaxLength(16);
            entity.Property(e => e.Name).HasMaxLength(64);
            entity.Property(e => e.PermissionId).HasColumnType("int(11)");
            entity.Property(e => e.ProfilePicturePath).HasMaxLength(64);
            entity.Property(e => e.Salt)
                .HasMaxLength(64)
                .HasColumnName("SALT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
