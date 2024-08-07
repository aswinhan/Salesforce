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
        private readonly IServiceListingService _serviceListingService;
        public ServiceListingController(IPractitionerService practitionerService, IMapper mapper, IOAuthService oAuthService, ISalesforceService salesforceService, ILogger<PractitionerController> logger, IServiceListingService serviceListingService)
        {
            _practitionerService = practitionerService;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _oAuthService = oAuthService;
            _salesforceService = salesforceService;
            _logger = logger;
            _serviceListingService = serviceListingService;
        }

        [Authorize(Roles = "admin,customer")]
        public async Task<IActionResult> Index()
        {
            try
            {
                string token = await _oAuthService.GetBearerTokenAsync();
                string serviceListing = await _serviceListingService.GetServiceListing(token);

                if (!string.IsNullOrEmpty(serviceListing))
                {
                    ViewBag.Message = "JSON body sent successfully!";
                    string formattedJson = JsonUtility.Format(serviceListing);
                    TempData["FormattedError"] = formattedJson;
                }
                else
                {
                    TempData["error"] = "Empty response received from Salesforce API.";
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching practitioner in Index method. {errorMessage}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }
    }
}
