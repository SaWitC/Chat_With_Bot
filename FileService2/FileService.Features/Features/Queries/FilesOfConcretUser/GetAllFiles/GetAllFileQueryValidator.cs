using FileServer.Features.Features.Queries.FilesOfConcretUser.GetAllFiles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Features.Features.Queries.FilesOfConcretUser.GetAllFiles
{
    public class GetAllFileQueryValidator:AbstractValidator<GetAllFilesQuery>
    {
        public GetAllFileQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
        }
    }
}
