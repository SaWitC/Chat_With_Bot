using FluentValidation;

namespace BotServer.Features.Features.Commands.Messages.BotResponse
{
    public class WriteResponseFromServerValidator : AbstractValidator<WriteResponseFromServer>
    {
        public WriteResponseFromServerValidator()
        {
            RuleFor(x => x.MessageFromUser).NotEmpty();

            RuleFor(x => x.ChatId).NotEmpty();
        }
    }
}
