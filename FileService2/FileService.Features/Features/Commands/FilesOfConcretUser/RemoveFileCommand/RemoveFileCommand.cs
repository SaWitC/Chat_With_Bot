using MediatR;

namespace FileServer.Features.Features.Commands.FilesOfConcretUser.RemoveFileCommand
{
    public class RemoveFileCommand : IRequest<bool>
    {
        public string UserId { get; set; }

        public string BlobName { get; set; }
    }
}
