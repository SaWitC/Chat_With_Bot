using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Chat.GetChatById
{
    public class GetChatByIdQueryValidator:AbstractValidator<GetChatByIdQuery>
    {
        public GetChatByIdQueryValidator()
        {
            RuleFor(x => x.page).InclusiveBetween(0, int.MaxValue).WithMessage("Incorrect page");

            RuleFor(x => x.id).NotEmpty();
        }
    }
}
