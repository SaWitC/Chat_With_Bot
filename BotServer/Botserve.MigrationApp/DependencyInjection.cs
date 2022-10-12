﻿using BotServer.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Botserve.MigrationApp
{
    public class DependencyInjection
    {
        public static void AddMigraion(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly("Botserve.MigrationApp"));
            });
        }
    }
}
