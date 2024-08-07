using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using SalesforceWeb.Dtos;
using SalesforceWeb.Models;
using SalesforceWeb.Services.IServices;
using SalesforceWeb.Utilities;

namespace SalesforceWeb.Controllers
{
    public class PractitionerController : Controller
    {
        private readonly IPractitionerService _practitionerService;
        private readonly IMapper _mapper;
        private readonly ISalesforceService _salesforceService;
        private readonly ILogger<PractitionerController> _logger;
        private readonly IToastNotification _toastNotification;


        public PractitionerController(IPractitionerService practitionerService,
             IMapper mapper, ISalesforceService salesforceService,
             ILogger<PractitionerController> logger,
             IToastNotification toastNotification
             )
        {
            _practitionerService = practitionerService;
            _mapper = mapper;
            _salesforceService = salesforceService;
            _logger = logger;
            _toastNotification = toastNotification;
        }


        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string credentialProfileId)
        {
            ActionResult<PractitionerFullDto> actionResult = await GetPractitioner(credentialProfileId);


            //Alert example 

            if (actionResult.Result == null)
            {
                _toastNotification.AddErrorToastMessage("Practitioner not found.");
                return View();
            }

            PractitionerFullDto model = actionResult.Value;
            ViewBag.credentialProfileId = credentialProfileId;
            return View(model);
        }

        public async Task<ActionResult<PractitionerFullDto>> GetPractitioner(string credentialProfileId)
        {
            try
            {
                var response = await _practitionerService.EditAsync<APIResponse>(credentialProfileId, HttpContext.Session.GetString(StaticData.SessionToken));

                if (response.Result != null && response.IsSuccess)
                {
                    PractitionerFullDto model = JsonConvert.DeserializeObject<PractitionerFullDto>(Convert.ToString(response.Result));
                    return _mapper.Map<PractitionerFullDto>(model);
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(response.ErrorMessages.FirstOrDefault());
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching practitioner with credentialProfileId {CredentialProfileId}: {ErrorMessage}", credentialProfileId, ex.Message);
                return StatusCode(500, "Internal server error occurred: " + ex.Message);
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


        //[HttpPost]
        //public async Task<IActionResult> PostCompositePractitioner(string jsonBody1, string jsonBody2)
        //{
        //    try
        //    {
        //        PractitionerFullDto payload2;

        //        try
        //        {
        //            payload2 = DeserializeWithFallback<PractitionerFullDto>(jsonBody2);
        //        }
        //        catch (JsonException ex)
        //        {
        //            ModelState.AddModelError("jsonBody", "Invalid JSON format.");
        //            _logger.LogError(ex, "Invalid JSON format: {ErrorMessage}", ex.Message);
        //            return View("Index");
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            ////Send JSON to Salesforce API and get response
        //            //string responseBody = await _salesforceService.PostToCompositeApiAsync(jsonBody1);

        //            //// Format and store response in TempData
        //            //string formattedJson = JsonUtility.Format(responseBody);
        //            //TempData["FormattedError"] = formattedJson;


        //            // Process the second payload
        //            var response2 = await _practitionerService.UpdateAsync<PractitionerFullDto>(
        //                payload2.practitionerPSVDtos.Credentialing_Profile__c,
        //                HttpContext.Session.GetString(StaticData.SessionToken),
        //                payload2
        //            );

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            // If model state is not valid, return to view with validation errors
        //            _logger.LogError("Validation Error: {ErrorMessage}", payload2);
        //            return View("Index", payload2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = $"Unexpected error occurred: {ex.Message}";
        //        _logger.LogError(ex, "Unexpected error occurred: {ErrorMessage}", ex.Message);
        //        return View("Index");
        //    }
        //}

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
                _logger.LogError("JSON file not found.");
                return Json(new { success = false, message = "JSON file not found." });
            }
        }

        private T DeserializeWithFallback<T>(string json)
        {
            var result = Activator.CreateInstance<T>();
            var errorList = new List<string>();

            JsonConvert.PopulateObject(json, result, new JsonSerializerSettings
            {
                Error = (sender, args) =>
                {
                    errorList.Add(args.ErrorContext.Member.ToString());
                    args.ErrorContext.Handled = true;
                }
            });

            if (errorList.Count > 0)
            {
                _logger.LogWarning("Some fields were not deserialized: {Fields}", string.Join(", ", errorList));
            }

            return result;
        }
    }
}
