using BotServer.Application.CustomHTTPClients;
using BotServer.Application.Services.Commands;
using BotServer.Services.CustomHTTPClients.Weather;
using Microsoft.Extensions.Configuration;
using OpenWeatherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.Services.Commands.WeatherCommands
{
    public class GetCurrentWeatherCommand : ICommandHandler
    {
        private readonly IWeatherHttpClient _weatherHttpClient;
        private readonly string[] location = new string[] {"Беларусь","Витебск","Москва","Питер" };
        private readonly IConfiguration _configuration;
        public GetCurrentWeatherCommand(IWeatherHttpClient weatherHttpClient,IConfiguration configuration)
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

            var openWeatherAPI = new OpenWeatherAPI.OpenWeatherApiClient(token);
            // Use async version wherever possible

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
                return $"Weather in {currentLocation}:\n" +
                    $"Temperature: {res.main.temp} \n" +
                    $"temp max: {res.main.temp_max}\n"+
                    $"temp min: {res.main.temp_min}\n";

            }

            return "Sory but I can't found any weather";
            



        }
    }
}
