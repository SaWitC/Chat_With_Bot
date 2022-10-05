using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Data
{
    public class DependencyInjection
    {
        public static void AddData(IConfiguration configurations ,IServiceCollection Services)
        {

            Services.AddSingleton(x => new BlobServiceClient(configurations.GetConnectionString("AzureBlobConnectionString")));

        }
    }
}
