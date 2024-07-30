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

namespace SalesforceWeb.Controllers
{
    public class CredentialingController : Controller
    {
        private readonly IPractitionerService _practitionerService;
        private readonly IMapper _mapper;
        private readonly ISalesforceService _salesforceService;
        private readonly ILogger<PractitionerController> _logger;
        public CredentialingController(IPractitionerService practitionerService, IMapper mapper, ISalesforceService salesforceService, ILogger<PractitionerController> logger)
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
                await PostCompositePractitioner(jsonBody);
                ViewBag.credentialProfileId = credentialProfileId;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching practitioner in Index method. {errorMessage}", ex.Message);
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
    }
}
