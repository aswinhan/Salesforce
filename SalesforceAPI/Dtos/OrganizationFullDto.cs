namespace SalesforceAPI.Dtos
{
    public class OrganizationFullDto
    {
        public CredentialingContactDto CredentialingContactDtos { get; set; }
        public ServiceLocationDto ServiceLocationDtos { get; set; }
        public ServiceLocationLicenseDto ServiceLocationLicenseDtos { get; set; }
        public OrganizationalPSVDto OrganizationalPSVDtos { get; set; }
        public DirectServiceDto DirectServiceDtos { get; set; }
        public OrganizationalCPDto OrganizationalCPDtos { get; set; }

    }
}
