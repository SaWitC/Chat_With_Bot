using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Messages.BotResponse
{
    public class WriteResponseFromServerValidator:AbstractValidator<WriteResponseFromServer>
    {
        public WriteResponseFromServerValidator()
        {
            RuleFor(x => x.MessageFromUser).NotEmpty();

            RuleFor(x => x.ChatId).NotEmpty();
        }
    }
}
