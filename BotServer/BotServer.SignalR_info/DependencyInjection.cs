using BotServer.Application.HubsAbstraction;
using BotServer.SignalR_info.Hubs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.SignalR_info
{
    public class DependencyInjection
    {
        public static void AddSignalR_Info(IServiceCollection Service)
        {
            //Service.AddScoped<INotifyHub, HubForNotify>();
        }
    }
}
