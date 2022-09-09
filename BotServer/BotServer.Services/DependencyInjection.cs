using BotServer.Application.Services.Commands;
using BotServer.Services.Services.Commands.HelloCommands;
using BotServer.Services.Services.Commands.WeatherCommands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services
{
    public class DependencyInjection
    {
        public static void AddServices(IServiceCollection Services)
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
        }
    }
}
