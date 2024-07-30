using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesforceAPI.Controllers.Services;
using SalesforceAPI.Data;
using SalesforceAPI.Dtos;
using SalesforceAPI.Models;
using System.Net;

namespace SalesforceAPI.Controllers
{
    [Authorize]
    [Route("Audit")]
    [ApiController]
    public class AuditController : Controller
    {

        protected APIResponse _response;
        private readonly ApplicationDbContext _context;
        private readonly ProviderService _providerService;
        private readonly IMapper _mapper;
        public AuditController(ApplicationDbContext context, IMapper mapper, ProviderService providerService)
        {
            _context = context;
            _providerService = providerService;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet(Name = "GetAditTrail")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAditTrail()
        {
            try
            {
                var auditTrail = await _context.AuditLogs
               .AsNoTracking()
               .Join(
                   _context.ApplicationUsers.AsNoTracking(),
                   audit => audit.UserId,
                   user => user.Id,
                   (audit, user) => new { audit, user }
               )
               .OrderByDescending(x => x.audit.DateTime)
               .Select(x => new AuditDto
               {
                   UserId = x.audit.UserId,
                   TableName = x.audit.TableName,
                   Type = x.audit.Type,
                   PrimaryKey = x.audit.PrimaryKey,
                   OldValues = x.audit.OldValues,
                   NewValues = x.audit.NewValues,
                   AffectedColumns = x.audit.AffectedColumns,
                   DateTime = x.audit.DateTime,
                   UserDto = new UserDto
                   {
                       ID = x.user.Id,
                       UserName = x.user.UserName,
                       Name = x.user.Name
                   }
               })
               .ToListAsync();

                if (auditTrail == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = auditTrail;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        private async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}