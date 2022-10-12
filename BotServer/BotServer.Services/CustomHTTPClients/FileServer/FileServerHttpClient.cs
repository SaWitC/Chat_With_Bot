using BotServer.Application.CustomHTTPClients;
using BotServer.Application.DataServices;
using BotServer.Data.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Services.CustomHTTPClients.FileServer
{
    [Service]
    public class FileServerHttpClient:IFileServiceCHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextService _httpContextService;

        private string _fileServerDomain = "";
        private string _fileServerBlobController = "";
        private string _fileServerBlobsController = "";



        public FileServerHttpClient(HttpClient httpClient, IConfiguration configuration, IHttpContextService httpContextService)
        {
            _httpContextService = httpContextService;
            _httpClient = httpClient;
            _configuration = configuration;
            if (_configuration != null)
            {
                _fileServerDomain = _configuration["FileServer:FileServerDomain"] != null ? _configuration["FileServer:FileServerDomain"] : "";
                _fileServerBlobController = _configuration["FileServer:FileServerBlobController"] != null ? _configuration["FileServer:FileServerBlobController"] : "";
                _fileServerBlobsController = _configuration["FileServer:FileServerBlobsController"] != null ? _configuration["FileServer:FileServerBlobsController"] : "";

            }
        }

        public async Task<string> GetAllFilesByUserId()
        {
            var userid = _httpContextService.GetCurentUserId();
            var authorizationHeader = _httpContextService.GetRequestHeader("Authorization");
            var uri = $"{_fileServerDomain}/{_fileServerBlobsController}";

            if (string.IsNullOrEmpty(authorizationHeader.Value))
            {
                return "";
            }
            var token = authorizationHeader.Value.ToString().Replace("Bearer", "").Trim();

            var request = new HttpRequestMessage(method: HttpMethod.Get, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using(var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<string>();
                }
                else
                {
                    return $"{response.StatusCode} server exception";
                }
            }
        }

        public async Task<FileStreamResult> GetFileByTitleByuserId(string blobName)
        {
            var userId =_httpContextService.GetCurentUserId();
            var uri = $"{_fileServerDomain}/{_fileServerBlobController}?blobName={blobName}";
            var authorizationHeader = _httpContextService.GetRequestHeader("Authorization");

            if (string.IsNullOrEmpty(authorizationHeader.Value))
            {
                return null;
            }
            var token = authorizationHeader.Value.ToString().Replace("Bearer", "").Trim();

            var request = new HttpRequestMessage(method: HttpMethod.Get, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //request.

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<FileStreamResult>();
                }
                else
                {
                    return null;
                }
            }
        }

        public Task<bool> RemoveFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            var userId = _httpContextService.GetCurentUserId();
            var uri = $"{_fileServerDomain}/{_fileServerBlobController}";
            var authorizationHeader = _httpContextService.GetRequestHeader("Authorization");

            if (string.IsNullOrEmpty(authorizationHeader.Value))
            {
                return false;
            }
            var token = authorizationHeader.Value.ToString().Replace("Bearer", "").Trim();

            var request = new HttpRequestMessage(method: HttpMethod.Post, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new MultipartFormDataContent();

            using var content = new MultipartFormDataContent
            {
                { new StreamContent(file.OpenReadStream()), "file", file.FileName }
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<bool>();
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
