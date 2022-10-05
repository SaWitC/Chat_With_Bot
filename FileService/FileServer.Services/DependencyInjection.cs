using FileServer.Application.Interfaces.Azure;
using FileServer.Services.Services.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Services
{
    public class DependencyInjection
    {
        public static void AddServices(IConfiguration configurations ,IServiceCollection Services)
        {
            Services.AddSingleton<IBlobService, BlobService>();
        }
    }
}
