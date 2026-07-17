using System.ComponentModel.DataAnnotations;

namespace DAFTech.DriverLicenseSystem.Api.Models
{
    public class VerificationLog
    {
        [Key]
        public int LogId { get; set; }
        public string LicenseId { get; set; }
        public string VerificationStatus { get; set; }
        public int CheckedBy { get; set; }
        public DateTime CheckedDate { get; set; }
    }
}
