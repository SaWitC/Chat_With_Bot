using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.ChatCommands.CreateChatCommand
{
    public class CreateChatCommandValidator:AbstractValidator<CreateChatCommand>
    {
        CreateChatCommandValidator()
        {
            RuleFor(x => x.createChatDTO.avtorId)
                .NotEmpty();

            RuleFor(x => x.createChatDTO.Title)
                .MinimumLength(5)
                .MaximumLength(25)
                .NotEmpty();
        }
    }
}
