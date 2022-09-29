using BotServer.Application.CustomHTTPClients;
using BotServer.Data.Attributes;
using BotServer.Services.CustomHTTPClients.Weather;
using BotServer.Services.CustomHTTPClients.Wiki;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotServer.Services
{
    public class DependencyInjection
    {
        public static void AddServices(IServiceCollection Services, IConfiguration configuration)
        {

            //Services.AddScoped<ICommandHandler, GetCurrentWeatherCommand>();
            //Services.AddScoped<ICommandHandler, HelloCommand>();
            //Services.AddScoped<ICommandHandler, GetArticleLinks>();
            //Services.AddScoped<ICommandHandler, RemindMeCommand>();
            //Services.AddScoped<ICommandHandler, RemindMeTaimeSaveCommand>();

            Services.Scan(scan => scan
                .FromAssemblyOf<startupServices>()
                    .AddClasses(classes => classes.WithAttribute(typeof(ServiceAttribute)))
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
