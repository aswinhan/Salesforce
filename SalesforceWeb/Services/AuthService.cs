﻿using SalesforceWeb.Models;
using SalesforceWeb.Dtos;
using SalesforceWeb.Services.IServices;
using SalesforceWeb.Services;
using SalesforceWeb.Utilities;

namespace MagicVilla_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string salesforceUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            salesforceUrl = configuration.GetValue<string>("ServiceUrls:SalesforceAPI");

        }

        public Task<T> LoginAsync<T>(LoginRequestDto obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = obj,
                Url = salesforceUrl + "/api/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDto obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = obj,
                Url = salesforceUrl + "/api/UsersAuth/register"
            });
        }
    }
}
