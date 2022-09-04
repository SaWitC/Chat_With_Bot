using BotServer.Application.Repositories;
using BotServer.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Data
{
    public class DependencyInjection
    {
        public static void AddData(IServiceCollection Services)
        {
            Services.AddScoped<IChatRepository, ChatRepository>();
            Services.AddScoped<IBaseRepository, BaseRepository>();
            Services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
