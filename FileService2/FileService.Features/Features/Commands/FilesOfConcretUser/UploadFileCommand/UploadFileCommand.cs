using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileServer.Features.Features.Commands.FilesOfConcretUser.UploadFileCommand
{
    public class UploadFileCommand : IRequest<bool>
    {
        public string UserId { get; set; }

        public IFormFile file { get; set; }
    }
}
