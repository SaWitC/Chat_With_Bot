using static BotServer.Domain.HttResponseModels.WeatherModels;

namespace BotServer.Application.CustomHTTPClients
{
    public interface IWeatherHttpClient
    {
        Task<Rootobject> GetWeather(string location);
    }
}
