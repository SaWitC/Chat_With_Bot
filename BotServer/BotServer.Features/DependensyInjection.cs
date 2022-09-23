using BotServer.Application.Repositories;
using BotServer.Data.MapperProfiles;
using BotServer.Data.Repositories;
using BotServer.Features.ValidationPipeline;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;
using Hangfire;
using BotServer.Features.BackgroundJobs.Remind;
using BotServer.Application.HubsAbstraction;

namespace BotServer.Features
{
    public class DependensyInjection
    {
        public static void AddFeatures(IServiceCollection Services,IConfiguration Configuration)
        {

            Services.AddSingleton<IVkApi>(sp =>
            {
                var api = new VkApi();
                api.Authorize(new ApiAuthParams() { AccessToken = Configuration["Config:AccessToken"] });
                return api;
            });

            //Services.AddHangfire(c=>c.UseSqlServerStorage);
            
           // Services.AddValidatorsFromAssembly(typeof(startup).Assembly);
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            Services.AddScoped<ISelectRepository,SelectRepository>();

            Services.AddMediatR(Assembly.GetExecutingAssembly());

            Services.AddAutoMapper(typeof(CustomMapperProfile));

            Services.AddHangfire(o => o.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            Services.AddHangfireServer();

            Services.AddScoped<IBGJobRemind,BGJobRemind>();
            //Services.AddScoped<IVkApi>(sp => {
            //    var api = new VkApi();
            //    //api.Token=""
            //    //api.Authorize(new ApiAuthParams { AccessToken = Configuration["VkConfig:token"]});
            //    return api;
            //});
        }
    }
}
