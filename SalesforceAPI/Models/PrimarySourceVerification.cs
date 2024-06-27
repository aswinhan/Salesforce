using System.ComponentModel.DataAnnotations;

namespace SalesforceAPI.Models
{
    public class PrimarySourceVerification
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string? CredentialingProfile { get; set; }
        public string? PrimarySourceVerifier { get; set; }
        public bool? CVO { get; set; }
        public string? OtherAccred { get; set; }
        public string? VerifiersredentialingOrganization { get; set; }
        public bool? IchatBackgroundheck { get; set; }
        public bool? WorkforceBackgroundheck { get; set; }
        public bool? MedicareBaseEnrollment { get; set; }
        public bool? MedicareOptOut { get; set; }
        public string? LARALicense { get; set; }
        public bool? LARAUploaded { get; set; }
        public bool? OfficialTranscriptfromAccreditedScho { get; set; }
        public bool? DegreeVerification { get; set; }
        public bool? AMAVerification { get; set; }
        public bool? AOAVerification { get; set; }
        public bool? MDHHSSanctionedProviderheck { get; set; }
        public bool? MCBAPVerification { get; set; }
        public bool? OfficeofInspectorGeneralheck { get; set; }
        public bool? SAMgovheck { get; set; }
        public bool? NationalSexOffenderRegistryheck { get; set; }
        public bool? MIPublicSexOffenderRegistryheck { get; set; }
        public bool? ECFMG { get; set; }
    }
}
