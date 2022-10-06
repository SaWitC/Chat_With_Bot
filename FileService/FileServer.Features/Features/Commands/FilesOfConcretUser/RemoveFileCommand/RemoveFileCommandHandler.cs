using FileServer.Application.Interfaces.Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Features.Features.Commands.FilesOfConcretUser.RemoveFileCommand
{
    public class RemoveFileCommandHandler : IRequestHandler<RemoveFileCommand, FileStreamResult>
    {
        private readonly IBlobService _blobService;
        public RemoveFileCommandHandler(IBlobService blobService)
        {
            _blobService = blobService;
        }
        public async Task<FileStreamResult> Handle(RemoveFileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //_blobService.RemoveFileAsync()
        }
    }
}
