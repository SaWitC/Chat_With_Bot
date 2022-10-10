
using FileServer.Domain.Models.Azure;
using Microsoft.AspNetCore.Http;

namespace FileServer.Application.Interfaces.Azure
{
    public interface IBlobService
    {
        public Task<IEnumerable<string>> GetAllBlobByUserIdAsync(string userId);
        public Task<AzureFileResponseModel> GetBlobByUserIdByTitle(string blobName, string userId);
        public Task<AzureFileResponseModel> GetBlobByUserIdByExtension(string extension, string userId);
        public Task UploadFileBlobAsync(string userId, IFormFile file);
        public Task<bool> RemoveFileAsync(string blobName);
    }
}
