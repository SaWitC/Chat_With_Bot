using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Reminds.getActualAndExpiredReminds
{
    public class GetActualAndExpiredRemindsValidator:AbstractValidator<GetActualAndExpiredRemindsQuery>
    {
        public GetActualAndExpiredRemindsValidator()
        {
            RuleFor(x => x.AvtorId).NotEmpty();
        }
    }
}
