using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Account.LoginCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountRepository _accountRepository;
        public LoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountRepository = accountRepository;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
                if (result.Succeeded)
                {
                    var token = await _accountRepository.GenerateJwtToken(user);
                    //_logger.LogInformation($"Loged {JsonSerializer.Serialize(user)}");

                    return token;
                    //return Ok(res);
                }
                if (result.IsNotAllowed)
                {

                }
            }

            return "";

        }
    }
}
