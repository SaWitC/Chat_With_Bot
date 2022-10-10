using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.DataServices
{
    public interface IHttpContextService
    {
        string GetCurentUserId();

        IEnumerable<KeyValuePair<string, StringValues>> GetRequestHeaders();

        KeyValuePair<string, StringValues> GetRequestHeader(string HeaderKey);
    }
}
