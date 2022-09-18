using BotServer.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botserve.MigrationApp
{
    public class DependencyInjection
    {
        public static void  AddMigraion(IServiceCollection Services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(connectionString, opt=>opt.MigrationsAssembly("Botserve.MigrationApp"));
                });
        }
    }
}
