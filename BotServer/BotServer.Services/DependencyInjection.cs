using BotServer.Application.CustomHTTPClients;
using BotServer.Data.Attributes;
using BotServer.Services.CustomHTTPClients.Weather;
using BotServer.Services.CustomHTTPClients.Wiki;
using BotServer.Services.SwaggerComplettedRealisation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotServer.Services
{
    public class DependencyInjection
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<startupServices>()
                    .AddClasses(classes => classes.WithAttribute(typeof(ServiceAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                );

            services.AddTransient<CustomClient>();


            services.AddHttpClient<WeatherHttpClient>("WeatherHttpClient");
            services.AddTransient<IWeatherHttpClient>(o =>
            {
                var clientFactory = o.GetRequiredService<IHttpClientFactory>();
                var httpClient = clientFactory.CreateClient("WeatherHttpClient");

                return new WeatherHttpClient(httpClient, configuration);
            });

            services.AddHttpClient<WikiHttpClient>("WikiHttpClient");
            services.AddTransient<IWikiHttpClient>(o =>
            {
                var clientFactory = o.GetRequiredService<IHttpClientFactory>();
                var httpClient = clientFactory.CreateClient("WikiHttpClient");

                return new WikiHttpClient(httpClient, configuration);
            });

            services.AddHttpContextAccessor();

        }
    }
}
