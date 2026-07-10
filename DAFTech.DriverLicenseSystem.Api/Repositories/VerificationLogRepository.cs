using DAFTech.DriverLicenseSystem.Api.Data;
using DAFTech.DriverLicenseSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DAFTech.DriverLicenseSystem.Api.Repositories
{
    public class VerificationLogRepository
    {
        private readonly DriverLicenseDbContext _context;

        public VerificationLogRepository(DriverLicenseDbContext context)
        {
            _context = context;
        }

        public async Task<VerificationLog> CreateLogAsync(VerificationLog log)
        {
            _context.VerificationLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<List<VerificationLog>> GetLogsByLicenseIdAsync(string licenseId)
        {
            return await _context.VerificationLogs
                .Where(v => v.LicenseId == licenseId)
                .OrderByDescending(v => v.CheckedDate)
                .ToListAsync();
        }

        public async Task<VerificationLog> GetLatestLogAsync(string licenseId)
        {
            return await _context.VerificationLogs
                .Where(v => v.LicenseId == licenseId)
                .OrderByDescending(v => v.CheckedDate)
                .FirstOrDefaultAsync();
        }
    }
}
