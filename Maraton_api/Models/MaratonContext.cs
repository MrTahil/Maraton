﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Maraton_api.Models;

public partial class MaratonContext : DbContext
{
    public MaratonContext()
    {
    }

    public MaratonContext(DbContextOptions<MaratonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Eredmenyek> Eredmenyeks { get; set; }

    public virtual DbSet<Futok> Futoks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Eredmenyek>(entity =>
        {
            entity.HasKey(e => new { e.Futo, e.Kor }).HasName("PRIMARY");

            entity.ToTable("eredmenyek");

            entity.HasIndex(e => e.Futo, "futo");

            entity.Property(e => e.Futo)
                .HasColumnType("int(11)")
                .HasColumnName("futo");
            entity.Property(e => e.Kor)
                .HasColumnType("int(11)")
                .HasColumnName("kor");
            entity.Property(e => e.Ido)
                .HasColumnType("int(11)")
                .HasColumnName("ido");

            entity.HasOne(d => d.FutoNavigation).WithMany(p => p.Eredmenyeks)
                .HasForeignKey(d => d.Futo)
                .HasConstraintName("eredmenyek_ibfk_1");
        });

        modelBuilder.Entity<Futok>(entity =>
        {
            entity.HasKey(e => e.Fid).HasName("PRIMARY");

            entity.ToTable("futok");

            entity.Property(e => e.Fid)
                .HasColumnType("int(11)")
                .HasColumnName("fid");
            entity.Property(e => e.Csapat)
                .HasColumnType("int(11)")
                .HasColumnName("csapat");
            entity.Property(e => e.Ffi).HasColumnName("ffi");
            entity.Property(e => e.Fnev)
                .HasColumnType("text")
                .HasColumnName("fnev");
            entity.Property(e => e.Szulev)
                .HasColumnType("int(11)")
                .HasColumnName("szulev");
            entity.Property(e => e.Szulho)
                .HasColumnType("int(11)")
                .HasColumnName("szulho");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
