using BotServer.Application.CustomHTTPClients;
using BotServer.Application.Services.Commands;
using BotServer.Data.Attributes;
using Microsoft.Extensions.Configuration;

namespace BotServer.Services.Services.Commands.WeatherCommands
{
    [Service]
    public class GetCurrentWeatherCommand : ICommandHandler
    {
        private readonly IWeatherHttpClient _weatherHttpClient;
        private readonly string[] location = new string[] { "Беларусь", "Витебск", "Москва", "Питер" };
        private readonly IConfiguration _configuration;
        public GetCurrentWeatherCommand(IWeatherHttpClient weatherHttpClient, IConfiguration configuration)
        {
            _weatherHttpClient = weatherHttpClient;
            _configuration = configuration;
        }
        public bool CanProcess(ICommand command)
        {
            return command.CommandString.ToLower().Contains("weather");
        }

        public async Task<string> ProcessCommand(ICommand command)
        {
            var currentLocation = "";

            var token = _configuration["OpenWeatherMap:Token"];

            foreach (var x in location)
            {
                if (command.CommandString.ToLower().Contains(x.ToLower()))
                    currentLocation = x;
            }
            if (string.IsNullOrEmpty(currentLocation))
                return "Write location for example 'weather in moscow'";
            var res = await _weatherHttpClient.GetWeather(currentLocation);

            if (res != null)
            {
                return $"Weather in {currentLocation}:</br>" +
                    $"Temperature: {res.main.temp} </br>" +
                    $"temp max: {res.main.temp_max}</br>" +
                    $"temp min: {res.main.temp_min}</br>";
            }

            return "Sory but I can't found any weather";




        }
    }
}
