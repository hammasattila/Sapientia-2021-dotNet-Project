using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
#nullable disable

namespace DataAccessLayer
{
    public partial class AppDbContext : IdentityDbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientPass> ClientPasses { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Gym> Gyms { get; set; }
        public virtual DbSet<Pass> Passes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string server = ConfigurationManager.AppSettings.Get("server");
                string database = ConfigurationManager.AppSettings.Get("database");
                string user = ConfigurationManager.AppSettings.Get("user");
                string pwd = ConfigurationManager.AppSettings.Get("pwd");
                string connectionString = $"server='{server}';database={database};user={user};pwd='{pwd}';";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.Barcode, "UQ__Clients__177800D3E2830F5E")
                    .IsUnique();

                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.IdCardNr)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.InsertedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientPass>(entity =>
            {
                entity.HasIndex(e => e.Barcode, "UQ__ClientPa__177800D3CBAE08E6")
                    .IsUnique();

                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfPurchase)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsValid)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SellingPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientPasses)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientPas__Clien__2CF2ADDF");

                entity.HasOne(d => d.Pass)
                    .WithMany(p => p.ClientPasses)
                    .HasForeignKey(d => d.PassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientPas__PassI__2DE6D218");
            });

            modelBuilder.Entity<Entry>(entity =>
            {
                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entries__ClientI__2645B050");

                entity.HasOne(d => d.Gym)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(d => d.GymId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entries__GymId__282DF8C2");

                entity.HasOne(d => d.Pass)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(d => d.PassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entries__PassId__2739D489");
            });

            modelBuilder.Entity<Gym>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pass>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ValidPerDay).HasDefaultValueSql("((1))");

                entity.Property(e => e.ValidUntil).HasDefaultValueSql("((24))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
