using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using static SalesforceAPI.Dtos.PractitionerLicensesCertificationDto;

namespace SalesforceAPI.Models
{
    public class PractitionerLicensesCertification
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string? RecordTypeId { get; set; }
        public string? CredentialingProfileId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? FileUploaded { get; set; }
        public LicenseCertificationStatusEnum? LicenseCertificationStatus { get; set; }
        public string? LicenseCertificationType { get; set; }
        public LicenseTypesLARAEnum? LicenseTypesLARA { get; set; }
        
    }
}
