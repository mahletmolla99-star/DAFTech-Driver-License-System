using Microsoft.AspNetCore.Mvc;
using DAFTech.DriverLicenseSystem.Api.Services;
using DAFTech.DriverLicenseSystem.Api.Models.DTOs;

namespace DAFTech.DriverLicenseSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationService _authService;

        public AuthController(AuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Username and password are required" });

            var (success, message, token) = await _authService.LoginAsync(request.Username, request.Password);

            if (!success)
                return Unauthorized(new { message });

            return Ok(new { message, token });
        }
    }
}
