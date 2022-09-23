using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Account.UpdatePersonalDataCommand
{
    public class UpdatePersonalDataCommandValidator:AbstractValidator<UpdatePersonalDataCommand>
    {
        public UpdatePersonalDataCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
