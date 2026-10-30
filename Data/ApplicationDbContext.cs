using InventoryItemEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryItemEFCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=.\SQLEXPRESS;Database=InventoryItemDB;Trusted_Connection=True;TrustServerCertificate=True;",
                    sqlOptions => sqlOptions.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>()
                .HasIndex(item => item.SKU)
                .IsUnique();

            modelBuilder.Entity<InventoryItem>()
                .Property(item => item.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InventoryItem>()
                .Property(item => item.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<InventoryItem>()
                .Property(item => item.SKU)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<InventoryItem>()
                .Property(item => item.Category)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
