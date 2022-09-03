using BotServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Account.RegistrationCommand
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, bool>
    {
        private readonly SignInManager<User> _signinManager;
        private readonly UserManager<User> _userManager;

        public RegistrationCommandHandler(SignInManager<User> signinManager, UserManager<User> userManager)
        {
            _signinManager = signinManager;
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
                    return true;
            }
            return false;
        }
    }
}
