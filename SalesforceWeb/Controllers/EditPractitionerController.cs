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
    public class EditPractitionerController : Controller
    {
        private readonly IPractitionerService _practitionerService;
        private readonly IMapper _mapper;
        private readonly ISalesforceService _salesforceService;
        private readonly ILogger<EditPractitionerController> _logger;

        public EditPractitionerController(IPractitionerService practitionerService, IMapper mapper, ISalesforceService salesforceService, ILogger<EditPractitionerController> logger)
        {
            _practitionerService = practitionerService;
            _mapper = mapper;
            _salesforceService = salesforceService;
            _logger = logger;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string credentialProfileId)
        {
            ActionResult<PractitionerFullDto> actionResult = await GetEditPractitioner(credentialProfileId);
            PractitionerFullDto model = actionResult.Value;
            ViewBag.credentialProfileId = credentialProfileId;
                return View(model);
        }

        public async Task<ActionResult<PractitionerFullDto>> GetEditPractitioner(string credentialProfileId)
        {
            try
            {
                var response = await _practitionerService.EditAsync<APIResponse>(credentialProfileId, HttpContext.Session.GetString(StaticData.SessionToken));

                if (response.Result != null && response.IsSuccess)
                {
                    PractitionerFullDto model = JsonConvert.DeserializeObject<PractitionerFullDto>(Convert.ToString(response.Result));
                    return _mapper.Map<PractitionerFullDto>(model);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching practitioner with credentialProfileId {CredentialProfileId}: {ErrorMessage}", credentialProfileId, ex.Message);
                return StatusCode(500, "Internal server error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCompositeEditPractitioner(string jsonBody)
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

        [HttpGet]
        public IActionResult GetJsonData()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "editPractitioner.json");

            if (System.IO.File.Exists(filePath))
            {
                string jsonData = System.IO.File.ReadAllText(filePath);
                return Json(new { success = true, data = jsonData });
            }
            else
            {
                _logger.LogError("JSON file not found.");
                return Json(new { success = false, message = "JSON file not found." });                
            }
        }
    }
}
