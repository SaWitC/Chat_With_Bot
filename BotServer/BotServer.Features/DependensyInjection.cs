using BotServer.Application.Repositories;
using BotServer.Data.MapperProfiles;
using BotServer.Data.Repositories;
using BotServer.Features.BackgroundJobs.Remind;
using BotServer.Features.ValidationPipeline;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

namespace BotServer.Features
{
    public class DependensyInjection
    {
        public static void AddFeatures(IServiceCollection Services, IConfiguration Configuration)
        {

            Services.AddSingleton<IVkApi>(sp =>
            {
                var api = new VkApi();
                api.Authorize(new ApiAuthParams() { AccessToken = Configuration["Config:AccessToken"] });
                return api;
            });

            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            Services.AddScoped<ISelectRepository, SelectRepository>();

            Services.AddMediatR(Assembly.GetExecutingAssembly());

            Services.AddAutoMapper(typeof(CustomMapperProfile));

            Services.AddHangfire(o => o.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            Services.AddHangfireServer();

            Services.AddScoped<IBGJobRemind, BGJobRemind>();
        }
    }
}
