namespace DAFTech.DriverLicenseSystem.Api.Models.DTOs
{
    public class VerifyLicenseRequestDto
    {
        public string LicenseId { get; set; }
        public string QRData { get; set; }
    }

    public class VerificationResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string LicenseId { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string ExpiryStatus { get; set; }
        public DateTime CheckedDate { get; set; }
    }
}
