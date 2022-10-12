using BotServer.Application.CustomHTTPClients;
using BotServer.Domain.HttResponseModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace BotServer.Services.CustomHTTPClients.Wiki
{
    public class WikiHttpClient : IWikiHttpClient
    {
        private IConfiguration _configuration;
        private HttpClient _httpClient;
        public WikiHttpClient(HttpClient HttpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = HttpClient;
            _httpClient.BaseAddress = new Uri("https://en.wikipedia.org");
            _httpClient.Timeout = new TimeSpan(0, 0, 10);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task<WikiModels.Rootobject> GetLinks(string requestText)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/w/api.php?action=query&origin=*&format=json&generator=search&gsrnamespace=0&gsrlimit=3&gsrsearch='{requestText}'");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var respstring = await response.Content.ReadAsStringAsync();
                    return Parse(respstring);
                }
                //return await response.Content.ReadAsAsync<WikiModels.Rootobject>();
                return null;
            }
        }

        private WikiModels.Rootobject Parse(string ResponseText)
        {
            string substr = Regex.Replace(ResponseText, @"""[0-9]*"":{", "{").Replace(@"""pages"":{{", @"""pages"":[{").Replace(@"}}}}", @"}]}}");

            return JsonConvert.DeserializeObject<WikiModels.Rootobject>(substr);
        }
    }
}
