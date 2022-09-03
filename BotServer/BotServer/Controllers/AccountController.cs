
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using BotServer.Features.Features.Account.LoginCommand;
using BotServer.Features.Features.Account.RegistrationCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BotServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly SignInManager<User> _signinManager;
        //private readonly UserManager<User> _userManager;
        private readonly IBaseRepository _baseRepository;
        //private readonly ILogger<AccountController> _logger;
        private readonly IMediator _mediatr;

        public AccountController(/*SignInManager<User> signInManager,UserManager<User> userManager,*/IBaseRepository baseRepository,IMediator mediator)
        {
            //_signinManager = signInManager;
            //_userManager = userManager;
            _baseRepository = baseRepository;
            _mediatr = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationCommand registrationCommand)
        {
            var res = await _mediatr.Send(registrationCommand);
            return Ok(res);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand registrationCommand)
        {
            var res = await _mediatr.Send(registrationCommand);
            return Ok(res);
        }

    }
}
