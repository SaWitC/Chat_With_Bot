using FluentValidation;

namespace BotServer.Features.Features.Queries.Reminds.getActualAndExpiredReminds
{
    public class GetActualAndExpiredRemindsValidator : AbstractValidator<GetActualAndExpiredRemindsQuery>
    {
        public GetActualAndExpiredRemindsValidator()
        {
            RuleFor(x => x.AvtorId).NotEmpty();
        }
    }
}
