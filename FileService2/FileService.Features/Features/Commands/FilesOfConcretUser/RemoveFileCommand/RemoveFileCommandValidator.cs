using FileServer.Features.Features.Commands.FilesOfConcretUser.RemoveFileCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Features.Features.Commands.FilesOfConcretUser.RemoveFileCommand
{
    public class RemoveFileCommandValidator:AbstractValidator<FileServer.Features.Features.Commands.FilesOfConcretUser.RemoveFileCommand.RemoveFileCommand>
    {
        public RemoveFileCommandValidator()
        {
            RuleFor(x=>x.UserId).NotEmpty().NotNull();

            RuleFor(x => x.FileTitle).NotEmpty().NotNull();
        }
    }
}
