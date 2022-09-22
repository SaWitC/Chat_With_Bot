using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Queries.Account.GetPersonalData
{
    public class GetPersonalDataQueryValidator:AbstractValidator<GetPersonalDataQuery>
    {
        public GetPersonalDataQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
        }
    }
}
