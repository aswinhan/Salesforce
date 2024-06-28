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
    public class PractitionerCredentialingProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ProviderService _providerService;
        private readonly ILogger<PractitionerCredentialingProfileController> _logger;
        private readonly IMapper _mapper;
        public PractitionerCredentialingProfileController(ApplicationDbContext context, ProviderService providerService, ILogger<PractitionerCredentialingProfileController> logger, IMapper mapper)
        {
            _context = context;
            _providerService = providerService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: PractitionerCredentialingProfileFull/5
        [HttpGet("PractitionerCredentialingProfileFull/{credentialingProfileId}")]
        public async Task<ActionResult> GetPractitionerCredentialingProfileFull(string credentialingProfileId)
        {
            try
            {
                int? providerId = await _providerService.GetProviderIdAsync(credentialingProfileId);

                if (providerId.HasValue)
                {
                    var education = await _context.Educations.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var postGraduateMedicalTraining = await _context.PostGraduateMedicalTrainings.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var hospitalAffiliation = await _context.HospitalAffiliations.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var practitionerPSV = await _context.PractitionerPrimarySourceVerifications.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var practitionerLicensesCertification = await _context.PractitionerLicensesCertifications.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var practitionerCredentialingProfile = await _context.PractitionerCredentialingProfiles.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);


                    var educationBody = _mapper.Map<EducationDto>(education);
                    var postGraduateMedicalTrainingBody = _mapper.Map<PostGraduateMedicalTrainingDto>(postGraduateMedicalTraining);
                    var hospitalAffiliationBody = _mapper.Map<HospitalAffiliationDto>(hospitalAffiliation);
                    var practitionerPSVBody = _mapper.Map<PractitionerPrimarySourceVerificationDto>(practitionerPSV);
                    var practitionerLicensesCertificationBody = _mapper.Map<PractitionerLicensesCertificationDto>(practitionerLicensesCertification);
                    var practitionerCredentialingProfileBody = _mapper.Map<PractitionerCredentialingProfileDto>(practitionerCredentialingProfile);

                    var PractitionerCredentialingProfileFullBody = new PractitionerCredentialProfileFullDto
                    {
                        PractitionerCredentialingProfileDtos = practitionerCredentialingProfileBody,
                        EducationDtos = educationBody,
                        PostGraduateMedicalTrainingDtos = postGraduateMedicalTrainingBody,
                        HospitalAffiliationDtos = hospitalAffiliationBody,
                        PractitionerLicensesCertificationDtos = practitionerLicensesCertificationBody,
                        practitionerPrimarySourceVerificationDtos = practitionerPSVBody
                    };

                    return new JsonResult(PractitionerCredentialingProfileFullBody);
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
