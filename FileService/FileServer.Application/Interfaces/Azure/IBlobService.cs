using Azure.Storage.Blobs.Models;
using FileServer.Domain.Models.Azure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Application.Interfaces.Azure
{
    public interface IBlobService
    {
        public Task<IEnumerable<string>> GetAllBlobByUserIdAsync(string userId);
        public Task<AzureFileResponseModel> GetBlobByUserIdByTitle(string blobName, string userId);
        public Task<AzureFileResponseModel> GetBlobByUserIdByExtension(string extension, string userId);
        public Task UploadFileBlobAsync(string userId, IFormFile file);
        public Task RemoveFileAsync(string blobName);
    }
}
