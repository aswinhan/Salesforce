using AutoMapper;
using SalesforceWeb.Dtos;
using SalesforceWeb.Models;

namespace SalesforceWeb.Profiles
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<PractitionerFull, PractitionerFullDto>();
            CreateMap<PractitionerFullDto, PractitionerFull>();

            CreateMap<OrganizationFull, OrganizationFullDto>();
            CreateMap<OrganizationFullDto, OrganizationFull>();
        }
    }
}
