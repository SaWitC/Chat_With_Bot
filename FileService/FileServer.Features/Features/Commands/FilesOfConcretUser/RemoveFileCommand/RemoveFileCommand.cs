using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Features.Features.Commands.FilesOfConcretUser.RemoveFileCommand
{
    public class RemoveFileCommand:IRequest<FileStreamResult>
    {
        public string UserId { get; set; }

        public string BlobName { get; set; }
    }
}
