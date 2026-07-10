using Microsoft.EntityFrameworkCore;
using DAFTech.DriverLicenseSystem.Api.Models;

namespace DAFTech.DriverLicenseSystem.Api.Data
{
    public class DriverLicenseDbContext : DbContext
    {
        public DriverLicenseDbContext(DbContextOptions<DriverLicenseDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<VerificationLog> VerificationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Users table
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Configure Drivers table
            modelBuilder.Entity<Driver>()
                .HasKey(d => d.DriverId);
            modelBuilder.Entity<Driver>()
                .HasIndex(d => d.LicenseId)
                .IsUnique();

            // Configure VerificationLogs table
            modelBuilder.Entity<VerificationLog>()
                .HasKey(v => v.LogId);
        }
    }
}
