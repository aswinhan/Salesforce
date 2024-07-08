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

namespace SalesforceWeb.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _salesforceService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly OAuthService _oAuthService;
        public OrganizationController(IOrganizationService salesforceService, IMapper mapper, OAuthService oAuthService)
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
                //_logger.LogError(ex, "Error occurred while fetching practitioner in Index method.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        public async Task<ActionResult<OrganizationFullDto>> GetOrganization(string credentialProfileId)
        {
            try
            {
                var response = await _salesforceService.GetAsync<APIResponse>(credentialProfileId);

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
                //_logger.LogError(ex, "Error occurred while fetching practitioner.");
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
    }
}
