using BotServer.Application.Repositories;
using BotServer.Data.Data;
using BotServer.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public static void AddData(IServiceCollection Services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(connectionString,opt=>opt.MigrationsAssembly("BotServer.Migrations"));
               



                });

            Services.AddScoped<IChatRepository, ChatRepository>();
            Services.AddScoped<IBaseRepository, BaseRepository>();
            Services.AddScoped<IAccountRepository, AccountRepository>();
            Services.AddScoped<IMessageRepository, MessageRepository>();

        }
    }
}
