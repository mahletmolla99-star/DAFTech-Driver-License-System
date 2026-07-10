using DAFTech.DriverLicenseSystem.Api.Models;
using DAFTech.DriverLicenseSystem.Api.Models.DTOs;
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

        public async Task<(bool Success, string Message, DriverResponseDto Driver)> RegisterDriverAsync(RegisterDriverDto request, int userId)
        {
            try
            {
                // Check if license already exists
                bool licenseExists = await _driverRepository.LicenseExistsAsync(request.LicenseId);
                if (licenseExists)
                {
                    var existingDriver = await _driverRepository.GetDriverByLicenseIdAsync(request.LicenseId);
                    string status = IsLicenseActive(existingDriver.ExpiryDate) ? "Active" : "Expired";
                    return (false, $"License already registered - Status: {status}", null);
                }

                // Create new driver
                var driver = new Driver
                {
                    LicenseId = request.LicenseId,
                    FullName = request.FullName,
                    DateOfBirth = request.DateOfBirth,
                    LicenseType = request.LicenseType,
                    ExpiryDate = request.ExpiryDate,
                    QRRawData = request.QRRawData,
                    OCRRawText = request.OCRRawText,
                    CreatedDate = DateTime.UtcNow,
                    RegisteredBy = userId
                };

                var createdDriver = await _driverRepository.CreateDriverAsync(driver);

                var response = new DriverResponseDto
                {
                    DriverId = createdDriver.DriverId,
                    LicenseId = createdDriver.LicenseId,
                    FullName = createdDriver.FullName,
                    DateOfBirth = createdDriver.DateOfBirth,
                    LicenseType = createdDriver.LicenseType,
                    ExpiryDate = createdDriver.ExpiryDate,
                    Status = IsLicenseActive(createdDriver.ExpiryDate) ? "Active" : "Expired",
                    CreatedDate = createdDriver.CreatedDate
                };

                return (true, "Driver registered successfully", response);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}", null);
            }
        }

        private bool IsLicenseActive(DateTime expiryDate)
        {
            return expiryDate > DateTime.UtcNow;
        }
    }
}
