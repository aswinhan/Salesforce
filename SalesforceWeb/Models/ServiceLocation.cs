using System.ComponentModel.DataAnnotations;
using static SalesforceWeb.Dtos.ServiceLocationDto;

namespace SalesforceWeb.Models
{
    public class ServiceLocation
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string? AccountSite { get; set; }
        public string? Account { get; set; }
        public string? CredentialingProfileId { get; set; }
        public DateTime? FacilityLicenseExpirationifapplicab { get; set; }
        public bool? FacilityLicenseifapplicable { get; set; }
        public string? FacilityLicenseNumberifapplicable { get; set; }
        public FacilityLicenseStatusIfApplicableCEnum? FacilityLicenseStatusifapplicable { get; set; }
        public string? HoursofOperation { get; set; }
        public AccomodationsAccessibilityCEnum? AccomodationsAccessibility { get; set; }
        public string? AccomodationsAccessibilityOther { get; set; }
        public bool? LicensedFacility { get; set; }
    }
}
