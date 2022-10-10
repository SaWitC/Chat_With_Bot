using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Features.Features.Commands.FilesOfConcretUser.UploadFileCommand
{
    public class UploadFileCommandValidator:AbstractValidator<FileServer.Features.Features.Commands.FilesOfConcretUser.UploadFileCommand.UploadFileCommand>
    {
        public UploadFileCommandValidator()
        {
            RuleFor(x => x.file).NotNull();

            RuleFor(x => x.file.Length).Custom((x, context) =>
            {
                if ((x/1024)/1024>30)
                {
                    context.AddFailure($"too big file size");
                }
            });
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
