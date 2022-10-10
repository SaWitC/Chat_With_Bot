using FileServer.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileServer.Migrations
{
    public class DependencyInjection
    {
        public static void AddMigrations(IConfiguration configuration, IServiceCollection Services)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly("FileService.Migrations"));
            });
        }
    }
}
