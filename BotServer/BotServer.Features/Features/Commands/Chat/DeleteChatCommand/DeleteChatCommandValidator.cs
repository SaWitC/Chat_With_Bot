using FluentValidation;

namespace BotServer.Features.Features.Commands.Chat.DeleteChatCommand
{
    public class DeleteChatCommandValidator : AbstractValidator<DeleteChatCommand>
    {
        public DeleteChatCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();
        }
    }
}
