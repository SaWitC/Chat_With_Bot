using FluentValidation;

namespace BotServer.Features.Features.Queries.Chat.GetChatById
{
    public class GetChatByIdQueryValidator : AbstractValidator<GetChatByIdQuery>
    {
        public GetChatByIdQueryValidator()
        {
            //RuleFor(x => x.page).InclusiveBetween(0, int.MaxValue).WithMessage("Incorrect page");

            RuleFor(x => x.id).NotEmpty();
        }
    }
}
