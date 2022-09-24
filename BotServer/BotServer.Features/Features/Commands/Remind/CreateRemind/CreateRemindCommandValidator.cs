using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Remind.CreateRemind
{
    public class CreateRemindCommandValidator:AbstractValidator<CreateRemindCommand>
    {
        public CreateRemindCommandValidator()
        {
            RuleFor(x => x.RemindMessage).MinimumLength(1);

            RuleFor(x=>x.RemindAtTime).Custom((x, context) =>
            {
                if (x>DateTime.Now.AddMinutes(1))
                {
                    context.AddFailure($"incorrect date");
                }
            });
        }
    }
}
