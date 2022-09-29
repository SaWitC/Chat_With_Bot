using BotServer.Application.CustomHTTPClients;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using static BotServer.Domain.HttResponseModels.WeatherModels;

namespace BotServer.Services.CustomHTTPClients.Weather
{
    public class WeatherHttpClient : IWeatherHttpClient
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public WeatherHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.openweathermap.org");
            _httpClient.Timeout = new TimeSpan(0, 0, 10);
            _httpClient.DefaultRequestHeaders.Clear();
            _configuration = configuration;
        }

        public async Task<Rootobject> GetWeather(string location)
        {
            var token = _configuration["OpenWeatherMap:Token"];
            var uri = $"/data/2.5/weather?q={location}&appid={token}";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Rootobject rootobject = new Rootobject();

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Rootobject>();
                }
            }


            // var openWeatherAPI = new OpenWeatherAPI.OpenWeatherApiClient(token);
            // Use async version wherever possible
            //var query = await openWeatherAPI.QueryAsync($"city/{location}");

            // or non-async version if needed for legacy code
            //var query = openWeatherAPI.Query($"city/{location}");

            return null;
        }
    }
}
