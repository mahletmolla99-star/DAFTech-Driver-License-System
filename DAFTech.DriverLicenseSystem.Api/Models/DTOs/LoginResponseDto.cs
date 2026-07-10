namespace DAFTech.DriverLicenseSystem.Api.Models.DTOs
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public UserDto User { get; set; }
    }

    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
    }
}
