using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;
using ChinookDataAccessLayer.Models;
using Microsoft.Extensions.Configuration;

namespace ChinookDataAccessLayer;
/// <summary>
/// Seperate class for DB context class inside the data access layer. Without having in the UI level. - SOC
/// </summary>
public partial class ChinookContext : IdentityDbContext<ChinookUserData>
{
    public ChinookContext()
    {
    }

    public ChinookContext(DbContextOptions<ChinookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlbumData> Albums { get; set; } = null!;
    public virtual DbSet<ArtistData> Artists { get; set; } = null!;
    public virtual DbSet<CustomerData> Customers { get; set; } = null!;
    public virtual DbSet<EmployeeData> Employees { get; set; } = null!;
    public virtual DbSet<GenreData> Genres { get; set; } = null!;
    public virtual DbSet<InvoiceData> Invoices { get; set; } = null!;
    public virtual DbSet<InvoiceLineData> InvoiceLines { get; set; } = null!;
    public virtual DbSet<MediaTypeData> MediaTypes { get; set; } = null!;
    public virtual DbSet<PlaylistData> Playlists { get; set; } = null!;
    public virtual DbSet<TrackData> Tracks { get; set; } = null!;
    public virtual DbSet<UserPlaylistData> UserPlaylists { get; set; } = null!;

    public virtual DbSet<UserPlaylistData> Save { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlbumData>(entity =>
        {
            entity.ToTable("Album");

            entity.HasIndex(e => e.ArtistId, "IFK_AlbumArtistId");

            entity.Property(e => e.AlbumId).ValueGeneratedNever();

            entity.Property(e => e.Title).HasColumnType("NVARCHAR(160)");

            entity.HasOne(d => d.Artist)
                .WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ArtistData>(entity =>
        {
            entity.ToTable("Artist");

            entity.Property(e => e.ArtistId).ValueGeneratedNever();

            entity.Property(e => e.Name).HasColumnType("NVARCHAR(120)");
        });

        modelBuilder.Entity<CustomerData>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.SupportRepId, "IFK_CustomerSupportRepId");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();

            entity.Property(e => e.Address).HasColumnType("NVARCHAR(70)");

            entity.Property(e => e.City).HasColumnType("NVARCHAR(40)");

            entity.Property(e => e.Company).HasColumnType("NVARCHAR(80)");

            entity.Property(e => e.Country).HasColumnType("NVARCHAR(40)");

            entity.Property(e => e.Email).HasColumnType("NVARCHAR(60)");

            entity.Property(e => e.Fax).HasColumnType("NVARCHAR(24)");

            entity.Property(e => e.FirstName).HasColumnType("NVARCHAR(40)");

            entity.Property(e => e.LastName).HasColumnType("NVARCHAR(20)");

            entity.Property(e => e.Phone).HasColumnType("NVARCHAR(24)");

            entity.Property(e => e.PostalCode).HasColumnType("NVARCHAR(10)");

            entity.Property(e => e.State).HasColumnType("NVARCHAR(40)");

            entity.HasOne(d => d.SupportRep)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.SupportRepId);
        });

        modelBuilder.Entity<EmployeeData>(entity =>
        {
            entity.ToTable("Employee");

            entity.HasIndex(e => e.ReportsTo, "IFK_EmployeeReportsTo");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();

            entity.Property(e => e.Address).HasColumnType("NVARCHAR(70)");

            entity.Property(e => e.BirthDate).HasColumnType("DATETIME");

            entity.Property(e => e.City).HasColumnType("NVARCHAR(40)");

            entity.Property(e => e.Country).HasColumnType("NVARCHAR(40)");

            entity.Property(e => e.Email).HasColumnType("NVARCHAR(60)");

            entity.Property(e => e.Fax).HasColumnType("NVARCHAR(24)");

            entity.Property(e => e.FirstName).HasColumnType("NVARCHAR(20)");

            entity.Property(e => e.HireDate).HasColumnType("DATETIME");

            entity.Property(e => e.LastName).HasColumnType("NVARCHAR(20)");

            entity.Property(e => e.Phone).HasColumnType("NVARCHAR(24)");

            entity.Property(e => e.PostalCode).HasColumnType("NVARCHAR(10)");

            entity.Property(e => e.State).HasColumnType("NVARCHAR(40)");

            entity.Property(e => e.Title).HasColumnType("NVARCHAR(30)");

            entity.HasOne(d => d.ReportsToNavigation)
                .WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo);
        });

        modelBuilder.Entity<GenreData>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).ValueGeneratedNever();

            entity.Property(e => e.Name).HasColumnType("NVARCHAR(120)");
        });

        modelBuilder.Entity<InvoiceData>(entity =>
        {
            entity.ToTable("Invoice");

            entity.HasIndex(e => e.CustomerId, "IFK_InvoiceCustomerId");

            entity.Property(e => e.InvoiceId).ValueGeneratedNever();

            entity.Property(e => e.BillingAddress).HasColumnType("NVARCHAR(70)");

            entity.Property(e => e.BillingCity).HasColumnType("NVARCHAR(40)");

            entity.Property(e => e.BillingCountry).HasColumnType("NVARCHAR(40)");

            entity.Property(e => e.BillingPostalCode).HasColumnType("NVARCHAR(10)");

            entity.Property(e => e.BillingState).HasColumnType("NVARCHAR(40)");

            entity.Property(e => e.InvoiceDate).HasColumnType("DATETIME");

            entity.Property(e => e.Total).HasColumnType("NUMERIC(10,2)");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<InvoiceLineData>(entity =>
        {
            entity.ToTable("InvoiceLine");

            entity.HasIndex(e => e.InvoiceId, "IFK_InvoiceLineInvoiceId");

            entity.HasIndex(e => e.TrackId, "IFK_InvoiceLineTrackId");

            entity.Property(e => e.InvoiceLineId).ValueGeneratedNever();

            entity.Property(e => e.UnitPrice).HasColumnType("NUMERIC(10,2)");

            entity.HasOne(d => d.Invoice)
                .WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Track)
                .WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MediaTypeData>(entity =>
        {
            entity.ToTable("MediaType");

            entity.Property(e => e.MediaTypeId).ValueGeneratedNever();

            entity.Property(e => e.Name).HasColumnType("NVARCHAR(120)");
        });

        modelBuilder.Entity<PlaylistData>(entity =>
        {
            entity.ToTable("Playlist");

            entity.Property(e => e.PlaylistId).ValueGeneratedNever();

            entity.Property(e => e.Name).HasColumnType("NVARCHAR(120)");

            entity.HasMany(d => d.Tracks)
                .WithMany(p => p.Playlists)
                .UsingEntity<Dictionary<string, object>>(
                    "PlaylistTrack",
                    l => l.HasOne<TrackData>().WithMany().HasForeignKey("TrackId").OnDelete(DeleteBehavior.ClientSetNull),
                    r => r.HasOne<PlaylistData>().WithMany().HasForeignKey("PlaylistId").OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("PlaylistId", "TrackId");

                        j.ToTable("PlaylistTrack");

                        j.HasIndex(new[] { "TrackId" }, "IFK_PlaylistTrackTrackId");
                    });
        });

        modelBuilder.Entity<TrackData>(entity =>
        {
            entity.ToTable("Track");

            entity.HasIndex(e => e.AlbumId, "IFK_TrackAlbumId");

            entity.HasIndex(e => e.GenreId, "IFK_TrackGenreId");

            entity.HasIndex(e => e.MediaTypeId, "IFK_TrackMediaTypeId");

            entity.Property(e => e.TrackId).ValueGeneratedNever();

            entity.Property(e => e.Composer).HasColumnType("NVARCHAR(220)");

            entity.Property(e => e.Name).HasColumnType("NVARCHAR(200)");

            entity.Property(e => e.UnitPrice).HasColumnType("NUMERIC(10,2)");

            entity.HasOne(d => d.Album)
                .WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AlbumId);

            entity.HasOne(d => d.Genre)
                .WithMany(p => p.Tracks)
                .HasForeignKey(d => d.GenreId);

            entity.HasOne(d => d.MediaType)
                .WithMany(p => p.Tracks)
                .HasForeignKey(d => d.MediaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });


        modelBuilder.Entity<UserPlaylistData>(entity =>
        {
            entity.ToTable("UserPlaylists");
            entity.HasKey(bc => new { bc.UserId, bc.PlaylistId });

            entity.HasOne(up => up.User)
                .WithMany(u => u.UserPlaylists)
                .HasForeignKey(up => up.UserId);

            entity.HasOne(up => up.Playlist)
                .WithMany(u => u.UserPlaylists)
                .HasForeignKey(up => up.PlaylistId);
        });

        OnModelCreatingPartial(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
