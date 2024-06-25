using System.ComponentModel.DataAnnotations;
using static SalesforceAPI.Dtos.OrganizationalPrimarySourceVerificationDto;

namespace SalesforceAPI.Models
{
    public class OrganizationalPrimarySourceVerification
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string? CredentialingProfile { get; set; }
        public string? PrimarySourceVerifier { get; set; }
        public bool? CVO { get; set; }
        public string? OtherAccred { get; set; }
        public VerifierSCredentialingOrganizationCEnum? VerifiersCredentialingOrganization { get; set; }
        public LARALicenseCEnum? LARALicense { get; set; }
        public bool? LARAUploaded { get; set; }
        public bool? ProofofAccreditation { get; set; }
        public bool? Disciplinarystatuswithregulatoryboar { get; set; }
        public bool? MDHHSSanctionedProviderCheck { get; set; }
        public bool? Atleastfiveyearhistoryoforganizati { get; set; }
        public bool? OnSiteQualityAssessmentRecredential { get; set; }
        public bool? OfficeofInspectorGeneralCheck { get; set; }
        public bool? SAMgovCheck { get; set; }
    }
}
