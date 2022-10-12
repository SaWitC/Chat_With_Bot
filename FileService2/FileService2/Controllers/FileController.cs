using Azure;
using Azure.Storage.Blobs;
using FileServer.Features.Features.Commands.FilesOfConcretUser.RemoveFileCommand;
using FileServer.Features.Features.Commands.FilesOfConcretUser.UploadFileCommand;
using FileServer.Features.Features.Queries.FilesOfConcretUser.GetAllFiles;
using FileServer.Features.Features.Queries.FilesOfConcretUser.GetFileByTitle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "get all files", OperationId = "GetAllFiles")]
        public async Task<IActionResult> GetBlobsAsync()
        {
            GetAllFilesQuery getAllFilesQuery = new();
            getAllFilesQuery.UserId = UserId;

            var res = await _mediatr.Send(getAllFilesQuery);

            return Ok(res);
        }

        [HttpGet("Blob/{blobName}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Get the file by blobName", OperationId = "GetFile")]
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
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Upload the file", OperationId = "UploadFile")]

        public async Task<IActionResult> UploadFileAsync()
        {
            try
            {
                UploadFileCommand command = new();
                var file = Request.Form.Files[0];
                command.file = file;
                command.UserId = UserId;
                await _mediatr.Send(command);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Ok();
        }

        [HttpDelete("Blob")]
       // [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Remove the file", OperationId = "RemoveFile")]
        public async Task<IActionResult> RemoveFileAsync(string blobName)
        {
            RemoveFileCommand command = new RemoveFileCommand();
            command.FileTitle = blobName;
            command.UserId = UserId;
            var res = await _mediatr.Send(command);
            if (res)
                return Ok();
            else
                return StatusCode(500,"File remove exception");
        }
    }
}
