using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesforceWeb.Dtos;
using SalesforceWeb.Models;
using SalesforceWeb.Services.IServices;
using SalesforceWeb.Utilities;

namespace SalesforceWeb.Controllers
{
    public class AuditController : Controller
    {
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;
        private readonly ISalesforceService _salesforceService;
        private readonly ILogger<EditPractitionerController> _logger;

        public AuditController(IAuditService auditService, IMapper mapper, ISalesforceService salesforceService, ILogger<EditPractitionerController> logger)
        {
            _auditService = auditService;
            _mapper = mapper;
            _salesforceService = salesforceService;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _auditService.GetAsync<APIResponse>(HttpContext.Session.GetString(StaticData.SessionToken));

                if (response.Result != null && response.IsSuccess)
                {
                    var model = JsonConvert.DeserializeObject<List<AuditDto>>(Convert.ToString(response.Result));
                    return View(model);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Audits : {ErrorMessage}", ex.Message);
                return StatusCode(500, "Internal server error occurred: " + ex.Message);
            }
        }
    }
}
