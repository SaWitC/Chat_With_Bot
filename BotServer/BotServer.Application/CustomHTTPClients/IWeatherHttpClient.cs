
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeatherAPI;
using static BotServer.Domain.HttResponseModels.WeatherModels;

namespace BotServer.Application.CustomHTTPClients
{
    public interface IWeatherHttpClient
    {
        Task<Rootobject> GetWeather(string location);
    }
}
