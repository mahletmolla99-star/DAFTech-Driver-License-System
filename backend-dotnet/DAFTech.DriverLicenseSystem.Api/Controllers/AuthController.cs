using Microsoft.AspNetCore.Mvc;
using DAFTech.DriverLicenseSystem.Api.Services;
using DAFTech.DriverLicenseSystem.Api.Models.DTOs;
using DAFTech.DriverLicenseSystem.Api.Models;
using DAFTech.DriverLicenseSystem.Api.Repositories;

namespace DAFTech.DriverLicenseSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationService _authService;
        private readonly UserRepository _userRepository;

        public AuthController(AuthenticationService authService, UserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Username and password are required" });

            var existingUser = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (existingUser != null)
                return BadRequest(new { message = "Username already exists" });

            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = UserRepository.HashPassword(request.Password),
                CreatedDate = DateTime.Now,
                Status = "Active"
            };

            await _userRepository.CreateUserAsync(newUser);
            return Ok(new { message = "User registered successfully" });
        }
    }
}
