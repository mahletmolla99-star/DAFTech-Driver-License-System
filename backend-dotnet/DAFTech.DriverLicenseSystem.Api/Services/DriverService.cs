using DAFTech.DriverLicenseSystem.Api.Models;
using DAFTech.DriverLicenseSystem.Api.Repositories;

namespace DAFTech.DriverLicenseSystem.Api.Services
{
    public class DriverService
    {
        private readonly DriverRepository _driverRepository;

        public DriverService(DriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<(bool success, string message, Driver? driver)> RegisterDriverAsync(Driver driver)
        {
            // Check if license already exists
            var exists = await _driverRepository.LicenseExistsAsync(driver.LicenseId);
            if (exists)
                return (false, "License already registered", null);

            // Create driver
            var newDriver = await _driverRepository.CreateDriverAsync(driver);
            return (true, "Driver registered successfully", newDriver);
        }

        public async Task<Driver?> GetDriverByLicenseAsync(string licenseId)
        {
            return await _driverRepository.GetDriverByLicenseIdAsync(licenseId);
        }
    }
}
