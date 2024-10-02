using AutoMapper;
using SalesforceWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using SalesforceWeb.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace SalesforceWeb.Controllers
{
    public class CredentialingController : Controller
    {
        private readonly IPractitionerService _practitionerService;
        private readonly IMapper _mapper;
        private readonly ISalesforceService _salesforceService;
        private readonly ILogger<CredentialingController> _logger;

        public CredentialingController(IPractitionerService practitionerService, IMapper mapper, ISalesforceService salesforceService, ILogger<CredentialingController> logger)
        {
            _practitionerService = practitionerService;
            _mapper = mapper;
            _salesforceService = salesforceService;
            _logger = logger;
        }

        [Authorize(Roles = "admin,customer")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string credentialProfileId)
        {
            if (string.IsNullOrEmpty(credentialProfileId))
            {
                _logger.LogWarning("Credential Profile ID is null or empty.");
                ViewBag.Error = "Credential Profile ID is required.";
                return View();
            }

            try
            {
                string jsonBody = $@"{{
                                ""allOrNone"": true,
                                ""compositeRequest"": [
                                    {{
                                        ""method"": ""GET"",
                                        ""referenceId"": ""thisCredentialingProfile"",
                                        ""url"": ""/services/data/v52.0/sobjects/Credentialing_Profile__c/{credentialProfileId}""
                                    }}
                                ]
                            }}";
                string responseBody = await PostCompositePractitioner(jsonBody);
                ViewBag.credentialProfileId = credentialProfileId;
                ViewBag.FormattedResponse = responseBody; // Pass JSON response to the view
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching practitioner in Index method. {errorMessage}", ex.Message);
                ViewBag.Error = "An error occurred while processing your request.";
                return View();
            }
        }

        [HttpPost]
        public async Task<string> PostCompositePractitioner(string jsonBody)
        {
            if (string.IsNullOrEmpty(jsonBody))
            {
                _logger.LogWarning("JSON body is null or empty.");
                ViewBag.Error = "Request body is required.";
                return string.Empty;
            }

            try
            {
                string responseBody = await _salesforceService.PostToCompositeApiAsync(jsonBody);
                string formattedJson = JsonUtility.Format(responseBody);
                return formattedJson;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while sending JSON body. {errorMessage}", ex.Message);
                ViewBag.Error = $"Unexpected error occurred. {ex.Message}";
                return string.Empty;
            }
        }

    }
}
