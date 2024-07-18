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
    [Route("EditPractitioner")]
    [ApiController]
    public class EditPractitionerController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ApplicationDbContext _context;
        private readonly ProviderService _providerService;
        private readonly IPractitionerRepository _dbPractitioner;
        private readonly IMapper _mapper;
        public EditPractitionerController(ApplicationDbContext context, IPractitionerRepository dbPractitioner, IMapper mapper, ProviderService providerService)
        {
            _context = context;
            _dbPractitioner = dbPractitioner;
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
                    var practitionerCP = await _context.PractitionerCredentialingProfiles.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);

                    var practitionerCPDto = _mapper.Map<PractitionerCPDto>(practitionerCP);
                    
                    if (practitionerCPDto == null)
                    {
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }

                    _response.Result = _mapper.Map<PractitionerCPDto>(practitionerCPDto);
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
