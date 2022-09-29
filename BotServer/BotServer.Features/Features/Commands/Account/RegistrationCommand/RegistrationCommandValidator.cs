using FluentValidation;

namespace BotServer.Features.Features.Account.RegistrationCommand
{
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();


            RuleFor(x => x.Password)
                .NotEmpty()
                .Equal(x => x.ConfirmPass);

            RuleFor(p => p.Password).Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.");
            RuleFor(p => p.Password).Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.");
            RuleFor(p => p.Password).Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
            RuleFor(x => x.Password).Matches(@"[\!\?\*\.]*$").WithMessage("Your password must contain at least one (!? *.).");


            RuleFor(x => x.ConfirmPass)
               .NotEmpty()
               .Equal(x => x.Password);


            RuleFor(x => x.UserName)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(25);
        }
    }
}
