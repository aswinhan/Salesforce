
using AutoMapper;
using SalesforceAPI.Dtos;
using SalesforceAPI.Models;
namespace SalesforceAPI.Profiles
{
    public class DtoProfile: Profile
    {
        public DtoProfile()
        {
            CreateMap<CredentialingContact, CredentialingContactDto>();
            CreateMap<DirectService, DirectServiceDto>();
            CreateMap<ServiceLocation, ServiceLocationDto>();
            CreateMap<ServiceLocationLicense, ServiceLocationLicenseDto>();
        }
    }
}
