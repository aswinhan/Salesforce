using AutoMapper;
using SalesforceWeb.Models;
using SalesforceWeb.Dtos;
using SalesforceWeb.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using SalesforceWeb.Utilities;
using SalesforceWeb.Services;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace SalesforceWeb.Controllers
{
    public class PractitionerController : Controller
    {
        private readonly IPractitionerService _salesforceService;
        private readonly IMapper _mapper;
        private readonly OAuthService _oAuthService;
        private readonly HttpClient _httpClient;
        public PractitionerController(IPractitionerService salesforceService, IMapper mapper, OAuthService oAuthService)
        {
            _salesforceService = salesforceService;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _oAuthService = oAuthService;
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
                var response = await _salesforceService.GetAsync<APIResponse>(credentialProfileId);

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
                string token = await _oAuthService.GetBearerTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string apiUrl = "https://mcal--mcsitnp.sandbox.my.salesforce.com/services/data/v59.0/composite/";

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "JSON body sent successfully!";
                }
                else
                {
                    ViewBag.Error = "Failed to send JSON body: " + response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
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
