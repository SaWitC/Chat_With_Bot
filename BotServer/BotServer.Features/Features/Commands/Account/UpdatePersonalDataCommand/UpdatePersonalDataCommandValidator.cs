using FluentValidation;

namespace BotServer.Features.Features.Commands.Account.UpdatePersonalDataCommand
{
    public class UpdatePersonalDataCommandValidator : AbstractValidator<UpdatePersonalDataCommand>
    {
        public UpdatePersonalDataCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
