using FileServer.Application.Interfaces.Azure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Features.Features.Queries.FilesOfConcretUser.GetAllFiles
{
    public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, string>
    {
        private readonly IBlobService _lobService;

        public GetAllFilesQueryHandler(IBlobService lobService)
        {
            _lobService = lobService;
        }

        public async Task<string> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
        {
            var res = await _lobService.GetAllBlobByUserIdAsync(request.UserId);

            string result = "";

            foreach (var item in res)
            {
                result += "\n" + res;
            }
            return result;

            return "ok";

        }
    }
}
