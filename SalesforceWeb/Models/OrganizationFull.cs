namespace SalesforceWeb.Models
{
    public class OrganizationFull
    {
        public CredentialingContact CredentialingContacts { get; set; }
        public ServiceLocation ServiceLocations { get; set; }
        public ServiceLocationLicense ServiceLocationLicenses { get; set; }
        public OrganizationalPSV OrganizationalPSVs { get; set; }
        public DirectService DirectServices { get; set; }
        public OrganizationalCP OrganizationalCPs { get; set; }

    }
}
