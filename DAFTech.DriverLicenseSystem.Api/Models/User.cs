namespace DAFTech.DriverLicenseSystem.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
