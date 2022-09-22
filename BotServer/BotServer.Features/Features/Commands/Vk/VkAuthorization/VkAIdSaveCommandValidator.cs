using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Vk.VkAuthorization
{
    public class VkAIdSaveCommandValidator:AbstractValidator<VkAIdSaveCommand>
    {
        VkAIdSaveCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

        }
    }
}
