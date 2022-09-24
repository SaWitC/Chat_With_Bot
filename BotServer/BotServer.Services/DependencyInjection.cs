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
using BotServer.Services.Services.Commands.WikiCommand;
using BotServer.Services.CustomHTTPClients.Wiki;
using BotServer.Services.Services.Commands.RemindCommands;
using VkNet.Abstractions;
using VkNet;

namespace BotServer.Services
{
    public class DependencyInjection
    {
        public static void AddServices(IServiceCollection Services,IConfiguration configuration)
        {
            
            //Services.AddScoped<ICommandHandler, GetCurrentWeatherCommand>();
            //Services.AddScoped<ICommandHandler, HelloCommand>();
            //Services.AddScoped<ICommandHandler, GetArticleLinks>();
            //Services.AddScoped<ICommandHandler, RemindMeCommand>();
            //Services.AddScoped<ICommandHandler, RemindMeTaimeSaveCommand>();

            Services.Scan(scan => scan
                .FromAssemblyOf<startupServices>()
                    .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Command", StringComparison.OrdinalIgnoreCase)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                );


            Services.AddHttpClient<WeatherHttpClient>("WeatherHttpClient");
            Services.AddTransient<IWeatherHttpClient>(o =>
            {
                var clientFactory = o.GetRequiredService<IHttpClientFactory>();
                var httpClient = clientFactory.CreateClient("WeatherHttpClient");

                return new WeatherHttpClient(httpClient, configuration);
            });

            Services.AddHttpClient<WikiHttpClient>("WikiHttpClient");
            Services.AddTransient<IWikiHttpClient>(o =>
            {
                var clientFactory = o.GetRequiredService<IHttpClientFactory>();
                var httpClient = clientFactory.CreateClient("WikiHttpClient");

                return new WikiHttpClient(httpClient, configuration);
            });
        }
    }
}
