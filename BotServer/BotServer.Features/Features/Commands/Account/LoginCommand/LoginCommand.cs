using MediatR;

namespace BotServer.Features.Features.Account.LoginCommand
{
    public class LoginCommand : IRequest<string>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
