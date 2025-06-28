using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AutoveloxProject.Models;

public partial class AutoveloxContext : DbContext
{
    public AutoveloxContext()
    {
    }

    public AutoveloxContext(DbContextOptions<AutoveloxContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comune> Comune { get; set; }

    public virtual DbSet<Mappa> Mappa { get; set; }

    public virtual DbSet<Provincium> Provincia { get; set; }

    public virtual DbSet<Regione> Regione { get; set; }

    public virtual DbSet<RipartizioneGeografica> RipartizioneGeografica { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Autovelox;User Id=its;Password=Its-2025;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comune>(entity =>
        {
            entity.HasKey(e => e.IdComune).HasName("PK_istat_Comuni_1");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Comunes).HasConstraintName("FK_Comune_Provincia");
        });

        modelBuilder.Entity<Mappa>(entity =>
        {
            entity.HasOne(d => d.IdComuneNavigation).WithMany(p => p.Mappas).HasConstraintName("FK_Mappa_Comune");
        });

        modelBuilder.Entity<Provincium>(entity =>
        {
            entity.Property(e => e.Sigla).IsFixedLength();

            entity.HasOne(d => d.IdRegioneNavigation).WithMany(p => p.Provincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provincia_Regione");
        });

        modelBuilder.Entity<Regione>(entity =>
        {
            entity.HasOne(d => d.IdRipartizioneGeograficaNavigation).WithMany(p => p.Regiones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Regione_RipartizioneGeografica");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
