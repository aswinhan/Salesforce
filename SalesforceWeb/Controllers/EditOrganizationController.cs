using AutoMapper;
using SalesforceWeb.Models;
using SalesforceWeb.Dtos;
using SalesforceWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using SalesforceWeb.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace SalesforceWeb.Controllers
{
    public class EditOrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        private readonly ISalesforceService _salesforceService;
        private readonly ILogger<EditOrganizationController> _logger;
        public EditOrganizationController(IOrganizationService organizationService, IMapper mapper, ISalesforceService salesforceService, ILogger<EditOrganizationController> logger)
        {
            _organizationService = organizationService;
            _mapper = mapper;
            _salesforceService = salesforceService;
            _logger = logger;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Index(string credentialProfileId)
        {
            try
            {
                ActionResult<OrganizationalCPDto> actionResult = await GetEditOrganization(credentialProfileId);
                OrganizationalCPDto model = actionResult.Value;
                ViewBag.credentialProfileId = credentialProfileId;
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Organization in Index method.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }
        public async Task<ActionResult<OrganizationalCPDto>> GetEditOrganization(string credentialProfileId)
        {
            try
            {
                var response = await _organizationService.EditAsync<APIResponse>(credentialProfileId, HttpContext.Session.GetString(StaticData.SessionToken));

                if (response.Result != null && response.IsSuccess)
                {
                    OrganizationalCPDto model = JsonConvert.DeserializeObject<OrganizationalCPDto>(Convert.ToString(response.Result));
                    return _mapper.Map<OrganizationalCPDto>(model);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Organization.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCompositeEditOrganization(string jsonBody)
        {
            try
            {
                string responseBody = await _salesforceService.PostToCompositeApiAsync(jsonBody);
                ViewBag.Message = "JSON body sent successfully!";
                string formattedJson = JsonUtility.Format(responseBody);
                TempData["FormattedError"] = formattedJson;
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Failed to send JSON body. {ex.Message}";
                TempData["Error"] = ex.Message;
            }
            catch (ApplicationException ex)
            {
                ViewBag.Error = $"Error communicating with Salesforce API. {ex.Message}";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Unexpected error occurred. {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetJsonData()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "editorganization.json");

            if (System.IO.File.Exists(filePath))
            {
                string jsonData = System.IO.File.ReadAllText(filePath);
                //string formattedJson = FormatJson(jsonData);
                //TempData["GeneratedJson"] = formattedJson;
                return Json(new { success = true, data = jsonData });
            }
            else
            {
                return Json(new { success = false, message = "JSON file not found." });
            }
        }

    }
}
