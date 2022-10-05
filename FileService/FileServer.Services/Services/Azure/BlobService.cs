using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileServer.Application.Interfaces.Azure;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Services.Services.Azure
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobService;

        public BlobService (BlobServiceClient blobServiceClient)
        {
            _blobService = blobServiceClient;
        }

        public async Task<BlobInfo> GetBlobAsync(string blobName)
        {
            //var containreClient = _blobService.GetBlobContainerClient();
            throw new NotImplementedException();

        }

        public async Task<IEnumerable<string>> GetBlobsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveFileAsync(string blobName)
        {
            throw new NotImplementedException();
        }

        public async Task UploadFileBlobAsync(string filePath, string fileName)
        {




            string id = "2131242";

            Console.WriteLine( _blobService.AccountName);
            var containerClient = _blobService.GetBlobContainerClient("blobsofusers");

            using(var stream =new FileStream("C:\\Users\\USER\\Downloads\\file.png",FileMode.Open))
            {
                Console.WriteLine(fileName);

                await containerClient.UploadBlobAsync($"{id}/file.png", stream) ;
                Console.WriteLine("Upladed");
            }

            var res = containerClient.GetBlobs();
            foreach (var item in res)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Metadata);
                Console.WriteLine("======================");
            }

        }
    }
}
