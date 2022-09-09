using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Chat.CreateChatCommand
{
    public class CreateChatCommandValidator:AbstractValidator<CreateChatCommand>
    {

        public CreateChatCommandValidator()
        {
            //RuleFor(x => x.avtorId).NotEmpty();

            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Chat title can not be chort that 5 subols");
            RuleFor(x => x.Title).MaximumLength(25).WithMessage("Chat title can not be longer that 25 subols");
        }
    }
}
