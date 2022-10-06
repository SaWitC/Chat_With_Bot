using FileServer.Application.Interfaces.Azure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Features.Features.Commands.FilesOfConcretUser.UploadFileCommand
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, bool>
    {
        private readonly IBlobService _blobService;
        public UploadFileCommandHandler(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task<bool> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            await _blobService.UploadFileBlobAsync(request.UserId, request.file);

            return true;
        }
    }
}
