using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotServer.Application.DataServices;
using Hangfire.Annotations;
using Microsoft.Extensions.Configuration;
using SwaggerCodegen;

namespace BotServer.Services.SwaggerComplettedRealisation
{
    public class CustomClient : Client
    {
        private readonly IConfiguration _configuration;

        private readonly IHttpContextService _httpContextService;
        public CustomClient(HttpClient _httpClient, IConfiguration configuration, IHttpContextService httpContextService , string baseUrl = "") : base(baseUrl, _httpClient)
        {
            _httpContextService = httpContextService;
            _configuration = configuration;
            if (_configuration!=null)
            {
                base.BaseUrl = _configuration["FileServer:BaseUri"];
            }
            var headers = _httpContextService.GetRequestHeaders();
        }
    }
}
