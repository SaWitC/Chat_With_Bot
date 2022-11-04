using BotServer.Application.DataServices;
using Microsoft.Extensions.Configuration;
using MyNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.SwaggerComplettedRealisation
{
    public class CustomBotRequestApiClient: BotRequestApiClient
    {
        private readonly IConfiguration _configuration;

        private readonly IHttpContextService _httpContextService;
        public CustomBotRequestApiClient(HttpClient _httpClient, IConfiguration configuration, IHttpContextService httpContextService, string baseUrl = "") : base(baseUrl, _httpClient)
        {
            _httpContextService = httpContextService;
            _configuration = configuration;
            if (_configuration != null)
            {
                base.BaseUrl = _configuration["BotRequestApi:BaseUri"];
            }
            var headers = _httpContextService.GetRequestHeaders();
        }
    }
}
