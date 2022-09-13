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
            Services.AddScoped<ICommandHandler, GetArticleLinks>();

            //Services.Scan(scan =>
            //{
            //    scan.FromAssemblyOf<ICommandHandler>()
            //    .AddClasses(classes => classes.AssignableTo<ICommandHandler>())
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime();
            //});

            //Services.Scan(scan => scan
            //  .FromAssemblyOf<ICommandHandler>()
            //    .AddClasses()
            //      .AsSelf()
            //      .WithTransientLifetime());


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
