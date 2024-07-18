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
    [Route("EditOrganization")]
    [ApiController]
    public class EditOrganizationController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ApplicationDbContext _context;
        private readonly ProviderService _providerService;
        private readonly IMapper _mapper;
        public EditOrganizationController(ApplicationDbContext context, IMapper mapper, ProviderService providerService)
        {
            _context = context;
            _providerService = providerService;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id}", Name = "GetEditOrganization")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEditOrganization(string id)
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
                    var organizationCP = await _context.OrganizationalCredentialingProfiles.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.ProviderId == providerId.Value);

                    var organizationalCPDto = _mapper.Map<OrganizationalCPDto>(organizationCP);


                    if (organizationalCPDto == null)
                    {
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }

                    _response.Result = _mapper.Map<OrganizationalCPDto>(organizationalCPDto);
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
