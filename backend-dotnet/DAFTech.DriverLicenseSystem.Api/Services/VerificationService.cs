using DAFTech.DriverLicenseSystem.Api.Models;
using DAFTech.DriverLicenseSystem.Api.Models.DTOs;
using DAFTech.DriverLicenseSystem.Api.Repositories;

namespace DAFTech.DriverLicenseSystem.Api.Services
{
    public class VerificationService
    {
        private readonly DriverRepository _driverRepository;
        private readonly VerificationLogRepository _logRepository;

        public VerificationService(DriverRepository driverRepository, VerificationLogRepository logRepository)
        {
            _driverRepository = driverRepository;
            _logRepository = logRepository;
        }

        public async Task<VerificationResultDto> VerifyLicenseAsync(VerifyLicenseRequestDto request, int userId)
        {
            try
            {
                var driver = await _driverRepository.GetDriverByLicenseIdAsync(request.LicenseId);
                
                if (driver == null)
                {
                    var fakeLog = new VerificationLog
                    {
                        LicenseId = request.LicenseId,
                        VerificationStatus = "Fake",
                        CheckedBy = userId,
                        CheckedDate = DateTime.UtcNow
                    };
                    await _logRepository.CreateLogAsync(fakeLog);

                    return new VerificationResultDto
                    {
                        Success = true,
                        Message = "License verification completed",
                        LicenseId = request.LicenseId,
                        Status = "Fake",
                        ExpiryStatus = "N/A",
                        CheckedDate = DateTime.UtcNow
                    };
                }

                bool isActive = driver.ExpiryDate > DateTime.UtcNow;
                string status = isActive ? "Real" : "Expired";
                string expiryStatus = isActive ? "Active" : "Expired";

                var log = new VerificationLog
                {
                    LicenseId = request.LicenseId,
                    VerificationStatus = status,
                    CheckedBy = userId,
                    CheckedDate = DateTime.UtcNow
                };
                await _logRepository.CreateLogAsync(log);

                return new VerificationResultDto
                {
                    Success = true,
                    Message = "License verification completed",
                    LicenseId = request.LicenseId,
                    FullName = driver.FullName,
                    Status = status,
                    ExpiryStatus = expiryStatus,
                    CheckedDate = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                return new VerificationResultDto
                {
                    Success = false,
                    Message = $"Verification error: {ex.Message}"
                };
            }
        }
    }
}
