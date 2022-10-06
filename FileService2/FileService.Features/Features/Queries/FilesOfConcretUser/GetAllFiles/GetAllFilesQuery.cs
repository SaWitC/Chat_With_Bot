using MediatR;

namespace FileServer.Features.Features.Queries.FilesOfConcretUser.GetAllFiles
{
    public class GetAllFilesQuery : IRequest<string>
    {
        public string UserId { get; set; }
    }
}
