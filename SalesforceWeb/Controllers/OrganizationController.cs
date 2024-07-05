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
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _salesforceService;
        private readonly IMapper _mapper;
        public OrganizationController(IOrganizationService salesforceService, IMapper mapper)
        {
            _salesforceService = salesforceService;
            _mapper = mapper;
        }

        [Route("Organization/GetOrganization/{credentialProfileId}")]
        public async Task<IActionResult> GetOrganization(string credentialProfileId)
        {
            var response = await _salesforceService.GetAsync<APIResponse>(credentialProfileId);
            if (response.Result != null && response.IsSuccess)
            {
                OrganizationFullDto model = JsonConvert.DeserializeObject<OrganizationFullDto>(Convert.ToString(response.Result));

                return View(_mapper.Map<OrganizationFullDto>(model));
            }
            return NotFound();

        }
    }
}
