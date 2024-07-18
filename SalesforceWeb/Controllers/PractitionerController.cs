using AutoMapper;
using SalesforceWeb.Models;
using SalesforceWeb.Dtos;
using SalesforceWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesforceWeb.Utilities;
using System.Text;

namespace SalesforceWeb.Controllers
{
    public class PractitionerController : Controller
    {
        private readonly IPractitionerService _practitionerService;
        private readonly IMapper _mapper;
        private readonly ISalesforceService _salesforceService;
        public PractitionerController(IPractitionerService practitionerService, IMapper mapper, ISalesforceService salesforceService)
        {
            _practitionerService = practitionerService;
            _mapper = mapper;
            _salesforceService = salesforceService;
        }

        public IActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Index(string credentialProfileId)
        {
            try
            {
                ActionResult<PractitionerFullDto> actionResult = await GetPractitioner(credentialProfileId);
                PractitionerFullDto model = actionResult.Value;
                ViewBag.credentialProfileId = credentialProfileId;
                return View(model);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while fetching practitioner in Index method.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        public async Task<ActionResult<PractitionerFullDto>> GetPractitioner(string credentialProfileId)
        {
            try
            {
                var response = await _practitionerService.GetAsync<APIResponse>(credentialProfileId, HttpContext.Session.GetString(StaticData.SessionToken));

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
                //_logger.LogError(ex, "Error occurred while fetching practitioner.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCompositePractitioner(string jsonBody)
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "practitioner.json");

            if (System.IO.File.Exists(filePath))
            {
                string jsonData = System.IO.File.ReadAllText(filePath);
                return Json(new { success = true, data = jsonData });
            }
            else
            {
                return Json(new { success = false, message = "JSON file not found." });
            }
        }
    }
}
