using Azure.Storage.Blobs;
using FileServer.Application.Interfaces.Azure;
using FileServer.Domain.Models.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FileServer.Services.Services.Azure
{
    public class BlobOfUsersService : IBlobService
    {
        private readonly BlobServiceClient _blobService;
        private readonly IConfiguration _configuration;

        public BlobOfUsersService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobService = blobServiceClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<string>> GetAllBlobByUserIdAsync(string userId)
        {
            var containerClient = _blobService.GetBlobContainerClient(_configuration["Azure:blobsofusers"]);

            var files = containerClient.GetBlobs();

            foreach (var file in files)
            {
                Console.WriteLine(file.Name);
                Console.WriteLine("==========================");
            }


            var files2 = containerClient.GetBlobsByHierarchy(prefix: userId);

            foreach (var file in files2)
            {
                Console.WriteLine(file.Blob.Name);
                Console.WriteLine("==========================");
            }
            return null;

        }

        public async Task<AzureFileResponseModel> GetBlobByUserIdByTitle(string blobName, string userId)
        {
            MemoryStream ms = new MemoryStream();
            if (CloudStorageAccount.TryParse(_configuration.GetConnectionString("AzureBlobConnectionString"), out CloudStorageAccount storageAccount))
            {
                CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = BlobClient.GetContainerReference(_configuration["Azure:blobsofusers"]);

                if (await container.ExistsAsync())
                {
                    CloudBlob file = container.GetBlobReference(userId+"/"+blobName);

                    if (await file.ExistsAsync())
                    {
                        await file.DownloadToStreamAsync(ms);
                        Stream blobStream = file.OpenReadAsync().Result;

                        return new AzureFileResponseModel() { ContentType = file.Properties.ContentType, FileName = file.Name, Filestream = blobStream };
                    }
                    else
                    {
                        throw new Exception("File does not exist");
                    }
                }
                else
                {
                    throw new Exception("container does not exist");
                }
            }
            else
            {
                throw new Exception("Error opening storage");
            }
            //return null;
        }

        public async Task<bool> RemoveFileAsync(string blobName)
        {
            var containerClient = _blobService.GetBlobContainerClient(_configuration["Azure:blobsofusers"]);

            var res = containerClient.DeleteBlobAsync(blobName);

            if (res.IsCompleted)
                return true;
            return false;
        }

        public async Task UploadFileBlobAsync(string UserId, IFormFile file)
        {
            try
            {
                var containerClient = _blobService.GetBlobContainerClient("blobsofusers");

                using (var fs = file.OpenReadStream())
                {
                    try
                    {
                        await containerClient.UploadBlobAsync($"{UserId}/{file.FileName}", fs);
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    Console.WriteLine("Upladed");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Task<AzureFileResponseModel> IBlobService.GetBlobByUserIdByExtension(string extension, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
