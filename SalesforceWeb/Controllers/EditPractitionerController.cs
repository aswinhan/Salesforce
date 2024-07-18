using AutoMapper;
using SalesforceWeb.Models;
using SalesforceWeb.Dtos;
using SalesforceWeb.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesforceWeb.Utilities;    
using System.Text;

namespace SalesforceWeb.Controllers
{
    public class EditPractitionerController : Controller
    {
        private readonly IPractitionerService _practitionerService;
        private readonly IMapper _mapper;
        private readonly ISalesforceService _salesforceService;
        public EditPractitionerController(IPractitionerService practitionerService, IMapper mapper, ISalesforceService salesforceService)
        {
            _practitionerService = practitionerService;
            _mapper = mapper;
            _salesforceService = salesforceService;
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
                ActionResult<PractitionerCPDto> actionResult = await GetEditPractitioner(credentialProfileId);
                PractitionerCPDto model = actionResult.Value;
                ViewBag.credentialProfileId = credentialProfileId;
                return View(model);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while fetching practitioner in Index method.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        public async Task<ActionResult<PractitionerCPDto>> GetEditPractitioner(string credentialProfileId)
        {
            try
            {
                var response = await _practitionerService.EditAsync<APIResponse>(credentialProfileId, HttpContext.Session.GetString(StaticData.SessionToken));

                if (response.Result != null && response.IsSuccess)
                {
                    PractitionerCPDto model = JsonConvert.DeserializeObject<PractitionerCPDto>(Convert.ToString(response.Result));
                    return _mapper.Map<PractitionerCPDto>(model);
                }
                else
                {
                    TempData["error"] = "No Records found";
                    return NotFound();                    
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while fetching practitioner.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]

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

        [Authorize(Roles = "admin")]
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
                return Json(new { success = false, message = "JSON file not found." });
            }
        }

    }
}
