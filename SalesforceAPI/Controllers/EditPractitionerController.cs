using AutoMapper;
using SalesforceAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SalesforceAPI.Models;
using SalesforceAPI.Controllers.Services;
using SalesforceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SalesforceAPI.Controllers
{
    [Authorize]
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

        [HttpPost("{id}", Name = "UpdateEditPractitioner")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdatePractitioner(string id, [FromBody] PractitionerFullDto practitionerFullDto)
        {
            var _response = new APIResponse();
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                int? providerId = await _providerService.GetProviderIdAsync(id);

                if (providerId.HasValue)
                {

                    var education = await _context.Educations.FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    if (education != null)
                    {
                        _mapper.Map(practitionerFullDto.EducationDtos, education);
                        _context.Entry(education).State = EntityState.Modified;
                    }

                    var pgMedicalTraining = await _context.PostGraduateMedicalTrainings.FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    if (pgMedicalTraining != null)
                    {
                        _mapper.Map(practitionerFullDto.PGMedicalTrainingDtos, pgMedicalTraining);
                        _context.Entry(pgMedicalTraining).State = EntityState.Modified;
                    }

                    var hospitalAffiliation = await _context.HospitalAffiliations.FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    if (hospitalAffiliation != null)
                    {
                        _mapper.Map(practitionerFullDto.HospitalAffiliationDtos, hospitalAffiliation);
                        _context.Entry(hospitalAffiliation).State = EntityState.Modified;
                    }

                    var practitionerPSV = await _context.PractitionerPrimarySourceVerifications.FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    if (practitionerPSV != null)
                    {
                        _mapper.Map(practitionerFullDto.practitionerPSVDtos, practitionerPSV);
                        _context.Entry(practitionerPSV).State = EntityState.Modified;
                    }

                    var practitionerLC = await _context.PractitionerLicensesCertifications.FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    if (practitionerLC != null)
                    {
                        _mapper.Map(practitionerFullDto.PractitionerLCDtos, practitionerLC);
                        _context.Entry(practitionerLC).State = EntityState.Modified;
                    }

                    var practitionerCP = await _context.PractitionerCredentialingProfiles.FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);
                    if (practitionerCP != null)
                    {
                        _mapper.Map(practitionerFullDto.PractitionerCPDtos, practitionerCP);
                        _context.Entry(practitionerCP).State = EntityState.Modified;
                    }

                    // Get current user
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
                    await _context.AuditEntityChangesAsync(user.Id);

                    _response.Result = practitionerFullDto;
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
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
