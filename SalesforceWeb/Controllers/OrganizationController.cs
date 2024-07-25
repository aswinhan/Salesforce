using AutoMapper;
using SalesforceWeb.Models;
using SalesforceWeb.Dtos;
using SalesforceWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using SalesforceWeb.Utilities;

namespace SalesforceWeb.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IOAuthService _oAuthService;
        private readonly ISalesforceService _salesforceService;
        private readonly ILogger<OrganizationController> _logger;
        public OrganizationController(IOrganizationService organizationService, IMapper mapper, IOAuthService oAuthService, ISalesforceService salesforceService, ILogger<OrganizationController> logger)
        {
            _organizationService = organizationService;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _oAuthService = oAuthService;
            _salesforceService = salesforceService;
            _logger = logger;
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
                string token = await _oAuthService.GetBearerTokenAsync();
                await GetServiceListing(token);
                await GetRelatedAccount(token, credentialProfileId);

                ActionResult<OrganizationFullDto> actionResult = await GetOrganization(credentialProfileId);
                OrganizationFullDto model = actionResult.Value;
                ViewBag.credentialProfileId = credentialProfileId;
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Organization in Index method.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        public async Task<ActionResult<OrganizationFullDto>> GetOrganization(string credentialProfileId)
        {
            try
            {
                var response = await _organizationService.GetAsync<APIResponse>(credentialProfileId, HttpContext.Session.GetString(StaticData.SessionToken));

                if (response.Result != null && response.IsSuccess)
                {
                    OrganizationFullDto model = JsonConvert.DeserializeObject<OrganizationFullDto>(Convert.ToString(response.Result));
                    return _mapper.Map<OrganizationFullDto>(model);
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

        public async Task<IActionResult> GetServiceListing(string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _httpClient.GetAsync("https://mcal--mcuatgcp.sandbox.my.salesforce.com/services/data/v59.0/query/?q=SELECT Id, Name FROM Service_Listing__c WHERE is_Certification__c = true AND Active__c = true");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        var result = JsonConvert.DeserializeObject<ServiceListingQueryResult>(responseBody);

                        ViewBag.ServiceList = result.records.Select(r => new SelectListItem
                        {
                            Value = r.Id,
                            Text = r.Name
                        }).ToList();

                        return View();
                    }
                    else
                    {
                        TempData["error"] = "Empty response received from Salesforce API.";
                        return Content("Empty response received from Salesforce API.");
                    }
                }
                else
                {
                    TempData["error"] = "Failed to fetch data from Salesforce API";
                    return Content("Failed to fetch data from Salesforce API: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return Content("Error: " + ex.Message);
            }
        }

        public async Task<IActionResult> GetRelatedAccount(string token, string Id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _httpClient.GetAsync("https://mcal--mcuatgcp.sandbox.my.salesforce.com/services/apexrest/api/Account/" + Id);


                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        var accounts = JsonConvert.DeserializeObject<List<RelatedAccount>>(responseBody);

                        ViewBag.AccountList = accounts.Select(a => new SelectListItem
                        {
                            Value = a.Id,
                            Text = a.Name
                        }).ToList();

                        return View();
                    }
                    else
                    {
                        return Content("Empty response received from Salesforce API.");
                    }
                }
                else
                {
                    return Content("Failed to fetch data from Salesforce API: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCompositeOrganization(string jsonBody)
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "organization.json");

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
