using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DAFTech.DriverLicenseSystem.Api.Models;

namespace DAFTech.DriverLicenseSystem.Api.Helpers
{
    public class JwtHelper
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationMinutes;

        public JwtHelper(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"] ?? "your-secret-key-here-min-32-characters-long";
            _issuer = configuration["Jwt:Issuer"] ?? "DriverLicenseApi";
            _audience = configuration["Jwt:Audience"] ?? "DriverLicenseClient";
            _expirationMinutes = int.Parse(configuration["Jwt:ExpirationMinutes"] ?? "60");
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Status", user.Status)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
