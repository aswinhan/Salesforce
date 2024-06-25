namespace SalesforceAPI.Dtos
{
    public class OrgCredentialProfileFullDto
    {
        public CredentialingContactDto? CredentialingContactDtos { get; set; }
        public ServiceLocationDto? ServiceLocationDtos { get; set; }
        public ServiceLocationLicenseDto? ServiceLocationLicenseDtos { get; set; }
        public OrganizationalPrimarySourceVerificationDto? OrganizationalPrimarySourceVerificationDtos { get; set; }
        public DirectServiceDto? DirectServiceDtos { get; set; }
        public OrganizationalCredentialingProfileDto? OrganizationalCredentialingProfileDtos { get; set; }

    }
}
