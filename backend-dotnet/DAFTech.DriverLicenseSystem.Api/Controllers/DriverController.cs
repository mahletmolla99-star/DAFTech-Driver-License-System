using Microsoft.AspNetCore.Mvc;
using DAFTech.DriverLicenseSystem.Api.Models;
using DAFTech.DriverLicenseSystem.Api.Models.DTOs;
using DAFTech.DriverLicenseSystem.Api.Data;

namespace DAFTech.DriverLicenseSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly DriverLicenseDbContext _context;

        public DriverController(DriverLicenseDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterDriver([FromBody] RegisterDriverDto request)
        {
            if (string.IsNullOrEmpty(request.LicenseId) || string.IsNullOrEmpty(request.FullName))
                return BadRequest(new { message = "License ID and Full Name are required" });

            var existing = _context.Drivers.FirstOrDefault(d => d.LicenseId == request.LicenseId);
            if (existing != null)
                return BadRequest(new { message = "License already registered" });

            var driver = new Driver
            {
                LicenseId = request.LicenseId,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                LicenseType = request.LicenseType,
                ExpiryDate = request.ExpiryDate,
                QRRawData = request.QRRawData,
                OCRRawText = request.OCRRawText,
                CreatedDate = DateTime.Now
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Driver registered successfully", licenseId = request.LicenseId });
        }
    }
}
