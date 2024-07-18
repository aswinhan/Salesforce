using AutoMapper;
using SalesforceWeb.Models;
using SalesforceWeb.Dtos;
using SalesforceWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesforceWeb.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Reflection;
using SalesforceWeb.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace SalesforceWeb.Controllers
{
    public class EditOrganizationController : Controller
    {
        private readonly IOrganizationService _salesforceService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly OAuthService _oAuthService;
        public EditOrganizationController(IOrganizationService salesforceService, IMapper mapper, OAuthService oAuthService)
        {
            _salesforceService = salesforceService;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _oAuthService = oAuthService;
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
                string token = await _oAuthService.GetBearerTokenAsync();

                ActionResult<OrganizationalCPDto> actionResult = await GetEditOrganization(credentialProfileId);
                OrganizationalCPDto? model = actionResult.Value;
                ViewBag.credentialProfileId = credentialProfileId;
                return View(model);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while fetching practitioner in Index method.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        

        public async Task<ActionResult<OrganizationalCPDto>> GetEditOrganization(string credentialProfileId)
        {
            try
            {
                var response = await _salesforceService.EditAsync<APIResponse>(credentialProfileId, HttpContext.Session.GetString(StaticData.SessionToken));

                if (response.Result != null && response.IsSuccess)
                {
                    OrganizationalCPDto? model = JsonConvert.DeserializeObject<OrganizationalCPDto>(Convert.ToString(response.Result));
                    return _mapper.Map<OrganizationalCPDto>(model);
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEditOrganization(string jsonBody)
        {
            try
            {
                string token = await _oAuthService.GetBearerTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string apiUrl = "https://mcal--mcuatgcp.sandbox.my.salesforce.com/services/data/v59.0/composite/";

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    ViewBag.Message = "JSON body sent successfully!";
                    string formattedJson = FormatJson(responseBody);
                    TempData["FormattedError"] = formattedJson;
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = "Failed to send JSON body. Status code: " + response.StatusCode + ", Reason: " + response.ReasonPhrase + ", Response: " + responseBody;
                    TempData["Error"] = responseBody;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
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

        private string FormatJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return "";

            try
            {
                dynamic parsedJson = JsonConvert.DeserializeObject(json);
                return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            }
            catch (JsonReaderException)
            {
                // Handle exception if JSON is invalid
                return "Invalid JSON format";
            }
        }
    }
}
