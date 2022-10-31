using BotServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BotServer.Features.Features.Account.RegistrationCommand
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegistrationCommandHandler> _logger;

        public RegistrationCommandHandler(
            ILogger<RegistrationCommandHandler> logger,
            UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<bool> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = new User() { UserName = request.UserName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var res = await _userManager.AddToRoleAsync(user, "user");
                if (res.Succeeded)
                    _logger.LogInformation($"registered new user {request.UserName}");
                return true;
            }
            return false;
        }
    }
}
