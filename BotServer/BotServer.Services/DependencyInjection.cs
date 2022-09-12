using BotServer.Application.CustomHTTPClients;
using BotServer.Application.Services.Commands;
using BotServer.Services.CustomHTTPClients.Weather;
using BotServer.Services.Services.Commands.HelloCommands;
using BotServer.Services.Services.Commands.WeatherCommands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace BotServer.Services
{
    public class DependencyInjection
    {
        public static void AddServices(IServiceCollection Services,IConfiguration configuration)
        {

            //Services.Scan(scan =>
            //{
            //    scan.FromCallingAssembly()
            //    .FromAssemblies(typeof(startup).Assembly)
            //    .AddClasses()
            //    .AsSelf();
            //});
            Services.AddScoped<ICommandHandler, GetCurrentWeatherCommand>();
            Services.AddScoped<ICommandHandler, HelloCommand>();

            // Services.AddScoped<IWeatherHttpClient, WeatherHttpClient>();
            Services.AddHttpClient<WeatherHttpClient>("WeatherHttpClient");
            Services.AddTransient<IWeatherHttpClient>(o =>
            {
                var clientFactory = o.GetRequiredService<IHttpClientFactory>();
                var httpClient = clientFactory.CreateClient("WeatherHttpClient");

                return new WeatherHttpClient(httpClient, configuration);
            });
        }
    }
}
