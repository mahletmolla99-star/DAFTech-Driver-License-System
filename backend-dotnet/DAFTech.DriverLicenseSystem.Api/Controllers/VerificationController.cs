using Microsoft.AspNetCore.Mvc;
using DAFTech.DriverLicenseSystem.Api.Models.DTOs;
using DAFTech.DriverLicenseSystem.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace DAFTech.DriverLicenseSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VerificationController : ControllerBase
    {
        private readonly DriverLicenseDbContext _context;

        public VerificationController(DriverLicenseDbContext context)
        {
            _context = context;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyLicense([FromBody] VerifyLicenseRequestDto request)
        {
            if (string.IsNullOrEmpty(request.LicenseId))
                return BadRequest(new { message = "License ID is required" });

            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.LicenseId == request.LicenseId);
            
            if (driver == null)
            {
                return Ok(new 
                { 
                    status = "Fake", 
                    message = "License not found in system", 
                    licenseId = request.LicenseId, 
                    fullName = "Unknown", 
                    expiryStatus = "NotFound" 
                });
            }

            if (driver.ExpiryDate != null && driver.ExpiryDate < DateTime.Now)
            {
                return Ok(new 
                { 
                    status = "Expired", 
                    message = "License has expired", 
                    licenseId = driver.LicenseId, 
                    fullName = driver.FullName, 
                    expiryStatus = "Expired" 
                });
            }

            return Ok(new 
            { 
                status = "Real", 
                message = "License is valid and active", 
                licenseId = driver.LicenseId, 
                fullName = driver.FullName, 
                expiryStatus = "Valid" 
            });
        }
    }
}
