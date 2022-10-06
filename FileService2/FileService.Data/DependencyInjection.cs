using Azure.Storage.Blobs;
using FileServer.Application.Interfaces.Repositories;
using FileServer.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileServer.Data
{
    public class DependencyInjection
    {
        public static void AddData(IConfiguration configurations, IServiceCollection Services)
        {

            Services.AddSingleton(x => new BlobServiceClient(configurations.GetConnectionString("AzureBlobConnectionString")));

            Services.AddScoped<IFileRepository, FileRepository>();
            //Services.AddTransient<IBaseRepository, BaseRepository>();


        }
    }
}
