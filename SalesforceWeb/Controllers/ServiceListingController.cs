using AutoMapper;
using SalesforceWeb.Models;
using SalesforceWeb.Dtos;
using SalesforceWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesforceWeb.Utilities;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http;
using SalesforceWeb.Services;

namespace SalesforceWeb.Controllers
{
    public class ServiceListingController : Controller
    {
        private readonly IPractitionerService _practitionerService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IOAuthService _oAuthService;
        private readonly ISalesforceService _salesforceService;
        private readonly ILogger<PractitionerController> _logger;
        public ServiceListingController(IPractitionerService practitionerService, IMapper mapper, IOAuthService oAuthService, ISalesforceService salesforceService, ILogger<PractitionerController> logger)
        {
            _practitionerService = practitionerService;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _oAuthService = oAuthService;
            _salesforceService = salesforceService;
            _logger = logger;
        }

        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> Index()
        {
            try
            {
                string token = await _oAuthService.GetBearerTokenAsync();
                await GetServiceListing(token);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching practitioner in Index method. {errorMessage}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }
        public async Task<IActionResult> GetServiceListing(string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _httpClient.GetAsync("https://mcal--mctraining.sandbox.my.salesforce.com/services/data/v59.0/query/?q=SELECT Id, Name FROM Service_Listing__c WHERE is_Certification__c = true AND Active__c = true");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        ViewBag.Message = "JSON body sent successfully!";
                        string formattedJson = JsonUtility.Format(responseBody);
                        TempData["FormattedError"] = formattedJson;
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
    }
}
