using FileServer.Domain.Models.Azure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Features.Features.Queries.FilesOfConcretUser.GetFileByTitle
{
    public class GetFileByTitleQuery:IRequest<AzureFileResponseModel>
    {
        public string UserId { get; set; }

        public string FileTitle { get; set; }
    }
}
