using FluentValidation;

namespace BotServer.Features.Features.Commands.Messages.SendMessage
{
    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(x => x.SendMessageDTO)
                .NotNull();

            RuleFor(x => x.SendMessageDTO.text).NotEmpty();

            RuleFor(x => x.SendMessageDTO.avtroId).NotEmpty();

            RuleFor(x => x.SendMessageDTO.ParentId).NotEmpty();
        }
    }
}
