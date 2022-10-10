using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Application.CustomHTTPClients
{
    public interface IFileServiceCHttpClient
    {

        public Task<string> GetAllFilesByUserId();
        public Task<bool> UploadFile(IFormFile file);

        public Task<bool> RemoveFile(string fileName);

        public Task<FileStreamResult> GetFileByTitleByuserId(string blobName);
    }
}
