using FluentValidation;

namespace BotServer.Features.Features.Account.LoginCommand
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.");
            RuleFor(p => p.Password).Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.");
            RuleFor(p => p.Password).Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
            RuleFor(x => x.Password).Matches(@"[\!\?\*\.]*$").WithMessage("Your password must contain at least one (!? *.).");

            RuleFor(x => x.UserName)
                .MinimumLength(5)
                .MaximumLength(25);

        }
    }
}
