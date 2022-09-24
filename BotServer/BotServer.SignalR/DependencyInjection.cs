using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using BotServer.Application.HubsAbstraction;
using BotServer.SignalR.Hubs;

namespace BotServer.SignalR
{
    public class DependencyInjection
    {

        
        public static void AddSignalR(IServiceCollection Services)
        {
            //serviceCollection.AddSignalRCore();
        }
    }
}
