using MediatR;

namespace BotServer.Features.Features.Account.RegistrationCommand
{
    public class RegistrationCommand : IRequest<bool>
    {
        public string VkEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPass { get; set; }
        public string Email { get; set; }
    }
}
