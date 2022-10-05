using FileServer.Application.Interfaces.Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IBlobService _lobService;
        public FileController(IBlobService blobService)
        {
            _lobService = blobService;
        }

        [HttpGet("Blob")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> GetBlobAsync()
        {
            //try
            //{
            //    var file = Request.Form.Files[0];


            //    if (file.FileName.Count(o => o == '.') == 1 && (Path.GetExtension(file.FileName) == ".gltf" || Path.GetExtension(file.FileName) == ".glb"))
            //    {
            //        //UploadFileCommand uploadFileCommand = new UploadFileCommand();
            //        //uploadFileCommand.file = file;
            //        //uploadFileCommand.id = id;
            //        //var res = await mediator.Send(uploadFileCommand);

            //        //if (res)
            //        //    return Ok();
            //        //else
            //        //    return StatusCode(500, $"server exception");
            //    }
            //    //return BadRequest();
            //    return BadRequest(new { err = "Incorrect file name or extension" });

            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"server exception {ex.Message}");
            //}


            //await _lobService.UploadFileBlobAsync("111","myfile");

            return Ok();
        }

        [HttpGet("Blobs")]
        public async Task<IActionResult> GetBlobsAsync(string blobName)
        {
            return Ok();
        }

        [HttpPost("Blob")]
        public async Task<IActionResult> UploadFileAsync()
        {
            return Ok();
        }

        //[HttpPost]
        //public async Task<IActionResult> GetAllFiles(string blobName)
        //{
        //    return Ok();
        //}

        [HttpDelete("Blob")]
        public async Task<IActionResult> RemoveFileAsync()
        {
            return Ok();
        }
    }
}
