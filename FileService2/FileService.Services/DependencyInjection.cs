using FileServer.Application.Interfaces.Azure;
using FileServer.Services.Services.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileServer.Services
{
    public class DependencyInjection
    {
        public static void AddServices(IConfiguration configurations, IServiceCollection Services)
        {
            Services.AddScoped<IBlobService, BlobOfUsersService>();
        }
    }
}
