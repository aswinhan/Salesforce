using System.ComponentModel.DataAnnotations;

namespace SalesforceAPI.Models
{
    public class PractitionerCredentialingProfile
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public DateTime? CertificateofLiabilityExpirationDate { get; set; }
        public DateTime? MalpracticeInsuranceCoverageExpiration { get; set; }
        public string? SummaryofChanges { get; set; }
    }
}
