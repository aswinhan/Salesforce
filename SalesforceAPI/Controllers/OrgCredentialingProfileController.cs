using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesforceAPI.Controllers.Services;
using SalesforceAPI.Data;
using SalesforceAPI.Dtos;
using SalesforceAPI.Models;

namespace SalesforceAPI.Controllers
{
    [ApiController]
    public class OrgCredentialingProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ProviderService _providerService;
        private readonly ILogger<OrgCredentialingProfileController> _logger;
        private readonly IMapper _mapper;
        public OrgCredentialingProfileController(ApplicationDbContext context, ProviderService providerService, ILogger<OrgCredentialingProfileController> logger, IMapper mapper)
        {
            _context = context;
            _providerService = providerService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: OrgCredentialingProfileFull/5
        [HttpGet("OrgCredentialingProfileFull/{credentialingProfileId}")]
        public async Task<ActionResult> GetOrgCredentialingProfileFull(string credentialingProfileId)
        {
            try
            {
                int? providerId = await _providerService.GetProviderIdAsync(credentialingProfileId);

                if (providerId.HasValue)
                {
                    var credentialingContact = await _context.CredentialingContacts.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var serviceLocation = await _context.ServiceLocations.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var serviceLocationLicense = await _context.ServiceLocationLicenses.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var organizationalPSV = await _context.OrganizationalPrimarySourceVerifications.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var directService = await _context.DirectServices.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var orgCredentialingProfile = await _context.OrganizationalCredentialingProfiles.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);

                    var credentialingContactBody = _mapper.Map<CredentialingContactDto>(credentialingContact);
                    var serviceLocationBody = _mapper.Map<ServiceLocationDto>(serviceLocation);
                    var serviceLocationLicenseBody = _mapper.Map<ServiceLocationLicenseDto>(serviceLocationLicense);
                    var organizationalPSVBody = _mapper.Map<OrganizationalPrimarySourceVerificationDto>(organizationalPSV);
                    var directServiceBody = _mapper.Map<DirectServiceDto>(directService);
                    var orgCredentialingProfileBody = _mapper.Map<OrganizationalCredentialingProfileDto>(orgCredentialingProfile);

                    var orgCredentialingProfileFullBody = new OrgCredentialProfileFullDto
                    {
                        CredentialingContactDtos = credentialingContactBody,
                        ServiceLocationDtos = serviceLocationBody,
                        ServiceLocationLicenseDtos = serviceLocationLicenseBody,
                        OrganizationalPrimarySourceVerificationDtos = organizationalPSVBody,
                        DirectServiceDtos = directServiceBody,
                        OrganizationalCredentialingProfileDtos = orgCredentialingProfileBody
                    };

                    return new JsonResult(orgCredentialingProfileFullBody);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the education record.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
