using BotServer.Data.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotServer.Data
{
    public class DependencyInjection
    {
        public static void AddData(IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<startupData>()
                    .AddClasses(classes => classes.WithAttribute(typeof(ServiceAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                );

        }
    }
}
