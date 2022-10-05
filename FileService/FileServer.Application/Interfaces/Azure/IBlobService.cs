using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Application.Interfaces.Azure
{
    public interface IBlobService
    {
        public Task<BlobInfo> GetBlobAsync(string blobName);
        public Task<IEnumerable<string>> GetBlobsAsync();
        public Task UploadFileBlobAsync(string filePath,string fileName);
        public Task RemoveFileAsync(string blobName);
    }
}
