using DAFTech.DriverLicenseSystem.Api.Models;
using DAFTech.DriverLicenseSystem.Api.Repositories;
using DAFTech.DriverLicenseSystem.Api.Helpers;

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

        public async Task<(bool success, string message, string? token)> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return (false, "User not found", null);

            if (!UserRepository.VerifyPassword(password, user.PasswordHash))
                return (false, "Invalid password", null);

            var token = _jwtHelper.GenerateToken(user.UserId, user.Username);
            return (true, "Login successful", token);
        }
    }
}
