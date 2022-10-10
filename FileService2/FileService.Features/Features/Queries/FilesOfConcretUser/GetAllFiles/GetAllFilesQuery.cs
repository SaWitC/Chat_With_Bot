using FileServer.Domain.Models.File;
using MediatR;

namespace FileServer.Features.Features.Queries.FilesOfConcretUser.GetAllFiles
{
    public class GetAllFilesQuery : IRequest<IEnumerable<FileModel>>
    {
        public string UserId { get; set; }
    }
}
