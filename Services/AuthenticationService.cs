using DAFTech.DriverLicenseSystem.Api.Models;
using DAFTech.DriverLicenseSystem.Api.Models.DTOs;
using DAFTech.DriverLicenseSystem.Api.Repositories;
using DAFTech.DriverLicenseSystem.Api.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace DAFTech.DriverLicenseSystem.Api.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthenticationService(UserRepository userRepository, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(request.Username);
                if (user == null)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Invalid username or password"
                    };
                }

                if (!VerifyPassword(request.Password, user.PasswordHash))
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Invalid username or password"
                    };
                }

                var token = _jwtHelper.GenerateToken(user);

                return new LoginResponseDto
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token,
                    User = new UserDto
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Status = user.Status
                    }
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput.Equals(hash);
        }
    }
}
