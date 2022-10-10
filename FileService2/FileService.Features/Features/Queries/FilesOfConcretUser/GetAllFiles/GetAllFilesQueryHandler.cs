using FileServer.Application.Interfaces.Azure;
using FileServer.Application.Interfaces.Repositories;
using FileServer.Domain.Models.File;
using MediatR;

namespace FileServer.Features.Features.Queries.FilesOfConcretUser.GetAllFiles
{
    public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, IEnumerable<FileModel>>
    {
        private readonly IBlobService _lobService;
        private readonly IFileRepository _fileRepository;

        public GetAllFilesQueryHandler(IBlobService lobService, IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            _lobService = lobService;
        }

        public async Task<IEnumerable<FileModel>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
        {
            var result = await _fileRepository.GetAllFilesByuserIdAsync(request.UserId);
            return result;
        }
    }
}
