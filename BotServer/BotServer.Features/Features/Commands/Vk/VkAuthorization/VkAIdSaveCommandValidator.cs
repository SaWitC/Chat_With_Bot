using FluentValidation;

namespace BotServer.Features.Features.Commands.Vk.VkAuthorization
{
    public class VkAIdSaveCommandValidator : AbstractValidator<VkAIdSaveCommand>
    {
        public VkAIdSaveCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

        }
    }
}
