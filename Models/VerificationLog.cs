namespace DAFTech.DriverLicenseSystem.Api.Models
{
    public class VerificationLog
    {
        public int LogId { get; set; }
        public string LicenseId { get; set; }
        public string VerificationStatus { get; set; }
        public int CheckedBy { get; set; }
        public DateTime CheckedDate { get; set; }
    }
}
