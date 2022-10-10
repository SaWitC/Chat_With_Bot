using BotServer.Application.DataServices;
using BotServer.Data.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.DataServices
{
    [Service]
    public class AccountService : IHttpContextService
    {
        private IHttpContextAccessor _accesor;

        public AccountService(IHttpContextAccessor accesor)
        {
            _accesor = accesor;
        }

        public string GetCurentUserId()
        {
            return _accesor.HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        public KeyValuePair<string,StringValues> GetRequestHeader(string HeaderKey)
        {
            var headers = _accesor.HttpContext.Request.Headers.ToList();
            return headers.FirstOrDefault(x => x.Key == HeaderKey);
        }

        public IEnumerable<KeyValuePair<string, StringValues>> GetRequestHeaders()
        {
            return _accesor.HttpContext.Request.Headers.ToList();
        }
    }
}
