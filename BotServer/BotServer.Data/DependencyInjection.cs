using BotServer.Data.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotServer.Data
{
    public class DependencyInjection
    {
        public static void AddData(IServiceCollection Services, IConfiguration configuration)
        {

            //Services.AddScoped<IChatRepository, ChatRepository>();
            //Services.AddScoped<IBaseRepository, BaseRepository>();
            //Services.AddScoped<IAccountRepository, AccountRepository>();
            //Services.AddScoped<IMessageRepository, MessageRepository>();
            //Services.AddScoped<IRemindRepository, RemindRepository>();
            //Services.AddScoped<IHubRepository, HubRepository>();

            Services.Scan(scan => scan
                .FromAssemblyOf<startupData>()
                    .AddClasses(classes => classes.WithAttribute(typeof(ServiceAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                );

        }
    }
}
