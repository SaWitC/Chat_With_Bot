using Azure.Storage.Blobs;
using FileServer.Features.Features.Commands.FilesOfConcretUser.UploadFileCommand;
using FileServer.Features.Features.Queries.FilesOfConcretUser.GetAllFiles;
using FileServer.Features.Features.Queries.FilesOfConcretUser.GetFileByTitle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileService.Controllers
{
    [Route("api/File")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IMediator _mediatr;
        public FileController(IMediator mediator, BlobServiceClient blobServiceClient)
        {
            _mediatr = mediator;
            _blobServiceClient = blobServiceClient;
        }
        public string UserId => HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

        [HttpGet("Blobs")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetBlobAsync()
        {
            GetAllFilesQuery getAllFilesQuery = new();
            getAllFilesQuery.UserId = UserId;

            var res = await _mediatr.Send(getAllFilesQuery);

            return Ok(res);
        }

        [HttpGet("Blob/{blobName}")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize]
        public async Task<IActionResult> GetBlobAsync(string blobName)
        {
            var x = Request.Headers.ToList();
            GetFileByTitleQuery query = new GetFileByTitleQuery();
            query.FileTitle = blobName;
            query.UserId = UserId;
            var res = await _mediatr.Send(query);
            return File(res.Filestream, res.ContentType, res.FileName);
        }

        [HttpPost("Blob")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> UploadFileAsync()
        {
            UploadFileCommand command = new();
            var file = Request.Form.Files[0];
            command.file = file;
            await _mediatr.Send(command);

            return Ok();
        }

        [HttpDelete("Blob")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveFileAsync()
        {
            return Ok();
        }
    }
}
