using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Features.Features.Commands.FilesOfConcretUser.UploadFileCommand
{
    public class UploadFileCommand:IRequest<bool>
    {
        public string UserId { get; set; }

        public IFormFile file { get; set; }
    }
}
