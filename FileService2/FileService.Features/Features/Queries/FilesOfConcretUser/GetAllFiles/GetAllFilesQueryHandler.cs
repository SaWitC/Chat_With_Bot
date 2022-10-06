using FileServer.Application.Interfaces.Azure;
using FileServer.Application.Interfaces.Repositories;
using MediatR;

namespace FileServer.Features.Features.Queries.FilesOfConcretUser.GetAllFiles
{
    public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, string>
    {
        private readonly IBlobService _lobService;
        private readonly IFileRepository _fileRepository;

        public GetAllFilesQueryHandler(IBlobService lobService, IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            _lobService = lobService;
        }

        public async Task<string> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
        {

            var titles = await _fileRepository.GetAllFilesTitlesByuserIdAsync(request.UserId);

            //var res = await _lobService.GetAllBlobByUserIdAsync(request.UserId);

            string result = "";

            foreach (var item in titles)
            {
                result += "\n" + item;
            }
            return result;
        }
    }
}
