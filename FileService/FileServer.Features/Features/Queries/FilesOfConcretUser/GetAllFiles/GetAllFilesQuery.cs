using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Features.Features.Queries.FilesOfConcretUser.GetAllFiles
{
    public class GetAllFilesQuery:IRequest<string>
    {
        public string UserId { get; set; }
    }
}
