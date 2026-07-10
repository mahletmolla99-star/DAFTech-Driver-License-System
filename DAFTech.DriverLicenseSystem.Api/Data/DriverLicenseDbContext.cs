using Microsoft.EntityFrameworkCore;
using DAFTech.DriverLicenseSystem.Api.Models;

namespace DAFTech.DriverLicenseSystem.Api.Data
{
    public class DriverLicenseDbContext : DbContext
    {
        public DriverLicenseDbContext(DbContextOptions<DriverLicenseDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<VerificationLog> VerificationLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=DriverLicenseDb;Integrated Security=true;TrustServerCertificate=true;");
            }
        }
    }
}
