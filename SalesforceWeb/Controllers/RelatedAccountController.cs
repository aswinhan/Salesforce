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
using NuGet.Common;

namespace SalesforceWeb.Controllers
{
    public class RelatedAccountController : Controller
    {
        private readonly IPractitionerService _practitionerService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IOAuthService _oAuthService;
        private readonly ISalesforceService _salesforceService;
        private readonly ILogger<PractitionerController> _logger;
        private readonly IRelatedAccountService _relatedAccountService;
        public RelatedAccountController(IPractitionerService practitionerService, IMapper mapper, IOAuthService oAuthService, ISalesforceService salesforceService, ILogger<PractitionerController> logger, IRelatedAccountService relatedAccountService)
        {
            _practitionerService = practitionerService;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _oAuthService = oAuthService;
            _salesforceService = salesforceService;
            _logger = logger;
            _relatedAccountService = relatedAccountService;
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
                string token = await _oAuthService.GetBearerTokenAsync();
                var responseBody = await _relatedAccountService.GetRelatedAccount(token, credentialProfileId);

                if (!string.IsNullOrEmpty(responseBody))
                {
                    ViewBag.Message = "JSON body sent successfully!";
                    string formattedJson = JsonUtility.Format(responseBody);
                    TempData["FormattedError"] = formattedJson;
                    ViewBag.credentialProfileId = credentialProfileId;
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Empty response received from Salesforce API.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error : {errorMessage}", ex.Message);
                return Content("Error: " + ex.Message);
            }
        }
    }
}
