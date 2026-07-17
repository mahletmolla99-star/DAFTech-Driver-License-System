namespace DAFTech.DriverLicenseSystem.Api.Models.DTOs
{
    public class RegisterDriverDto
    {
        public string LicenseId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseType { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string QRRawData { get; set; }
        public string OCRRawText { get; set; }
    }

    public class DriverResponseDto
    {
        public int DriverId { get; set; }
        public string LicenseId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseType { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
