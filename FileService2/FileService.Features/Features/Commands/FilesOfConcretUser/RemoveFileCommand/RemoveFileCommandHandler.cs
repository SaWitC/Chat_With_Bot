using FileServer.Application.Interfaces.Azure;
using FileServer.Application.Interfaces.Repositories;
using MediatR;

namespace FileServer.Features.Features.Commands.FilesOfConcretUser.RemoveFileCommand
{
    public class RemoveFileCommandHandler : IRequestHandler<RemoveFileCommand, bool>
    {
        private readonly IBlobService _blobService;
        private readonly IFileRepository _fileRepository;
        public RemoveFileCommandHandler(IBlobService blobService,IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            _blobService = blobService;
        }
        public async Task<bool> Handle(RemoveFileCommand request, CancellationToken cancellationToken)
        {
            var res =await _blobService.RemoveFileAsync(request.UserId+"/"+request.FileTitle);
            if (res)
            {
                var removeRes =await _fileRepository.RemoveFileAsync(request.FileTitle);
                if (removeRes != null)
                {
                    await _fileRepository.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
