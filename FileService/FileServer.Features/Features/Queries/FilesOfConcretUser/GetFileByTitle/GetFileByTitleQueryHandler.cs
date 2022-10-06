using FileServer.Application.Interfaces.Azure;
using FileServer.Domain.Models.Azure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Features.Features.Queries.FilesOfConcretUser.GetFileByTitle
{
    public class GetFileByTitleQueryHandler : IRequestHandler<GetFileByTitleQuery, AzureFileResponseModel>
    {
        private readonly IBlobService _lobService;
        public GetFileByTitleQueryHandler(IBlobService blobService)
        {
            _lobService = blobService;
        }
        public async Task<AzureFileResponseModel> Handle(GetFileByTitleQuery request, CancellationToken cancellationToken)
        {
            return await _lobService.GetBlobByUserIdByTitle(request.FileTitle,request.UserId);
        }
    }
}
