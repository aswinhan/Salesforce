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

namespace SalesforceWeb.Controllers
{
    public class PractitionerController : Controller
    {
        private readonly IPractitionerService _salesforceService;
        private readonly IMapper _mapper;
        public PractitionerController(IPractitionerService salesforceService, IMapper mapper)
        {
            _salesforceService = salesforceService;
            _mapper = mapper;
        }

        [Route("Practitioner/GetPractitioner/{credentialProfileId}")]
        public async Task<IActionResult> GetPractitioner(string credentialProfileId)
        {
            var response = await _salesforceService.GetAsync<APIResponse>(credentialProfileId);
            if (response.Result != null && response.IsSuccess)
            {
                PractitionerFullDto model = JsonConvert.DeserializeObject<PractitionerFullDto>(Convert.ToString(response.Result));

                return View(_mapper.Map<PractitionerFullDto>(model));
            }
            return NotFound();
        }
    }
}
