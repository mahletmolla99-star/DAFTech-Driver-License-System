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

        public async Task<Driver> GetDriverByLicenseIdAsync(string licenseId)
        {
            return await _context.Drivers.FirstOrDefaultAsync(d => d.LicenseId == licenseId);
        }

        public async Task<Driver> GetDriverByIdAsync(int driverId)
        {
            return await _context.Drivers.FirstOrDefaultAsync(d => d.DriverId == driverId);
        }

        public async Task<Driver> CreateDriverAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<bool> LicenseExistsAsync(string licenseId)
        {
            return await _context.Drivers.AnyAsync(d => d.LicenseId == licenseId);
        }

        public async Task<List<Driver>> GetAllDriversAsync()
        {
            return await _context.Drivers.ToListAsync();
        }
    }
}
