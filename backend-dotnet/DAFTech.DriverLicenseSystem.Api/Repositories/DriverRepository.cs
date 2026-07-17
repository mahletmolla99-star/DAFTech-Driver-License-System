using DAFTech.DriverLicenseSystem.Api.Data;
using DAFTech.DriverLicenseSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DAFTech.DriverLicenseSystem.Api.Repositories
{
    public class DriverRepository
    {
        private readonly DriverLicenseDbContext _context;

        public DriverRepository(DriverLicenseDbContext context)
        {
            _context = context;
        }

        // Get driver by License ID
        public async Task<Driver?> GetDriverByLicenseIdAsync(string licenseId)
        {
            return await _context.Drivers.FirstOrDefaultAsync(d => d.LicenseId == licenseId);
        }

        // Check if license already exists
        public async Task<bool> LicenseExistsAsync(string licenseId)
        {
            return await _context.Drivers.AnyAsync(d => d.LicenseId == licenseId);
        }

        // Create a new driver
        public async Task<Driver> CreateDriverAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }
    }
}
