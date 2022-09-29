using FluentValidation;

namespace BotServer.Features.Features.Queries.Account.GetPersonalData
{
    public class GetPersonalDataQueryValidator : AbstractValidator<GetPersonalDataQuery>
    {
        public GetPersonalDataQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
        }
    }
}
