using FluentValidation;

namespace BotServer.Features.Features.Queries.Messages.GetMessagesQuery
{
    public class GetMessagesQueryValidator : AbstractValidator<GetMessagesQuery>
    {
        public GetMessagesQueryValidator()
        {
            RuleFor(x => x.page).Custom((x, context) =>
            {
                if (x < 0)
                {
                    context.AddFailure($"{x} is not a valid number or less than 0");
                }
            });

            RuleFor(x => x.id).NotNull().NotEmpty();
        }
    }
}
