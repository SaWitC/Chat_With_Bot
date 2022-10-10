using FileServer.Features.Features.Queries.FilesOfConcretUser.GetFileByTitle;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Features.Features.Queries.FilesOfConcretUser.GetFileByTitle
{
    public class GetFileByTitleQueryValidator:AbstractValidator<GetFileByTitleQuery>
    {
        public GetFileByTitleQueryValidator()
        {
            RuleFor(x => x.FileTitle).NotEmpty().NotNull();
            RuleFor(x=>x.UserId).NotEmpty().NotNull();
        }
    }
}
