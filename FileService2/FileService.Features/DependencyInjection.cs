using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FileServer.Features
{
    public class DependencyInjection
    {
        public static void AddFeatures(IConfiguration configurations, IServiceCollection Services)
        {
            Services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
