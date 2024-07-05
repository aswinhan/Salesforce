using AutoMapper;
using SalesforceAPI.Dtos;
using SalesforceAPI.Repository.IRepostiory;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SalesforceWeb.Models;
using SalesforceAPI.Controllers.Services;
using SalesforceAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SalesforceAPI.Controllers
{
    [Route("Organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ApplicationDbContext _context;
        private readonly ProviderService _providerService;
        private readonly IOrganizationRepository _db;
        private readonly IMapper _mapper;
        public OrganizationController(ApplicationDbContext context, IOrganizationRepository db, IMapper mapper, ProviderService providerService)
        {
            _context = context;
            _db = db;
            _providerService = providerService;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id}", Name = "GetOrganization")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetOrganization(string id)
        {
            try
            {
                if (id == null || id == "")
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                int? providerId = await _providerService.GetProviderIdAsync(id);

                if (providerId.HasValue)
                {
                    var credentialingContact = await _context.CredentialingContacts.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var serviceLocation = await _context.ServiceLocations.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var serviceLocationLicense = await _context.ServiceLocationLicenses.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var organizationPSV = await _context.OrganizationalPrimarySourceVerifications.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var directService = await _context.DirectServices.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var organizationCP = await _context.OrganizationalCredentialingProfiles.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);

                    var credentialingContactDto = _mapper.Map<CredentialingContactDto>(credentialingContact);
                    var serviceLocationDto = _mapper.Map<ServiceLocationDto>(serviceLocation);
                    var serviceLocationLicenseDto = _mapper.Map<ServiceLocationLicenseDto>(serviceLocationLicense);
                    var organizationalPSVDto = _mapper.Map<OrganizationalPSVDto>(organizationPSV);
                    var directServiceDto = _mapper.Map<DirectServiceDto>(directService);
                    var organizationalCPDto = _mapper.Map<OrganizationalCPDto>(organizationCP);

                    var organizationFullDto = new OrganizationFullDto
                    {
                        CredentialingContactDtos = credentialingContactDto,
                        ServiceLocationDtos = serviceLocationDto,
                        ServiceLocationLicenseDtos = serviceLocationLicenseDto,
                        OrganizationalPSVDtos = organizationalPSVDto,
                        DirectServiceDtos = directServiceDto,
                        OrganizationalCPDtos = organizationalCPDto
                    };

                    if (organizationFullDto == null)
                    {
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }

                    _response.Result = _mapper.Map<OrganizationFullDto>(organizationFullDto);
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
