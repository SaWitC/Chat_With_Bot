using FileServer.Domain.Models.Azure;
using MediatR;

namespace FileServer.Features.Features.Queries.FilesOfConcretUser.GetFileByTitle
{
    public class GetFileByTitleQuery : IRequest<AzureFileResponseModel>
    {
        public string UserId { get; set; }

        public string FileTitle { get; set; }
    }
}
