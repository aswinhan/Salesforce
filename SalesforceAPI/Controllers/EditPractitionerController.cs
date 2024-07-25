using AutoMapper;
using SalesforceAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SalesforceAPI.Models;
using SalesforceAPI.Controllers.Services;
using SalesforceAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SalesforceAPI.Controllers
{
    [Route("EditPractitioner")]
    [ApiController]
    public class EditPractitionerController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ApplicationDbContext _context;
        private readonly ProviderService _providerService;
        private readonly IMapper _mapper;
        public EditPractitionerController(ApplicationDbContext context, IMapper mapper, ProviderService providerService)
        {
            _context = context;
            _providerService = providerService;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id}", Name = "GetEditPractitioner")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEditPractitioner(string id)
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
                    var education = await _context.Educations.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var pgMedicalTraining = await _context.PostGraduateMedicalTrainings.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var hospitalAffiliation = await _context.HospitalAffiliations.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var practitionerPSV = await _context.PractitionerPrimarySourceVerifications.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var practitionerLC = await _context.PractitionerLicensesCertifications.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    var practitionerCP = await _context.PractitionerCredentialingProfiles.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);


                    var educationDto = _mapper.Map<EducationDto>(education);
                    var pgMedicalTrainingDto = _mapper.Map<PGMedicalTrainingDto>(pgMedicalTraining);
                    var hospitalAffiliationDto = _mapper.Map<HospitalAffiliationDto>(hospitalAffiliation);
                    var practitionerPSVDto = _mapper.Map<PractitionerPSVDto>(practitionerPSV);
                    var practitionerLCDto = _mapper.Map<PractitionerLCDto>(practitionerLC);
                    var practitionerCPDto = _mapper.Map<PractitionerCPDto>(practitionerCP);

                    var PractitionerFullDto = new PractitionerFullDto
                    {
                        PractitionerCPDtos = practitionerCPDto,
                        EducationDtos = educationDto,
                        PGMedicalTrainingDtos = pgMedicalTrainingDto,
                        HospitalAffiliationDtos = hospitalAffiliationDto,
                        PractitionerLCDtos = practitionerLCDto,
                        practitionerPSVDtos = practitionerPSVDto
                    };

                    if (PractitionerFullDto == null)
                    {
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }

                    _response.Result = _mapper.Map<PractitionerFullDto>(PractitionerFullDto);
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
