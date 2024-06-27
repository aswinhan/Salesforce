using System.ComponentModel.DataAnnotations;

namespace SalesforceAPI.Models
{
    public class HospitalAffiliation
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string? CredentialingProfileId { get; set; }
        public string? HospitalAffiliationAddress { get; set; }
        public string? HospitalAffiliationName { get; set; }
        public string? CategoryofMembership { get; set; }
        public DateTime? StartDateofAffiliation { get; set; }
        public DateTime? EndDateofAffiliation { get; set; }
    }
}
