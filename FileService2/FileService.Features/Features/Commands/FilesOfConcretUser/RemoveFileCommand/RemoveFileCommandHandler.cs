using FileServer.Application.Interfaces.Azure;
using MediatR;

namespace FileServer.Features.Features.Commands.FilesOfConcretUser.RemoveFileCommand
{
    public class RemoveFileCommandHandler : IRequestHandler<RemoveFileCommand, bool>
    {
        private readonly IBlobService _blobService;
        public RemoveFileCommandHandler(IBlobService blobService)
        {
            _blobService = blobService;
        }
        public async Task<bool> Handle(RemoveFileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //_blobService.RemoveFileAsync()
        }
    }
}
