using BotServer.Data.Attributes;
using BotServer.Data.Repositories;
using BotServer.Domain.Models;
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
                    .WithScopedLifetime()
                );



            //services.AddScoped(typeof(ChatRepository));

        }
    }
}
