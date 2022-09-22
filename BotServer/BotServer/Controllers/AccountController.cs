
using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using BotServer.Features.Features.Account.LoginCommand;
using BotServer.Features.Features.Account.RegistrationCommand;
using BotServer.Features.Features.Queries.Account.GetPersonalData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.Swagger;
using System.Security.Claims;

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

        public Guid UserId => Guid.Parse(HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

        public AccountController(/*SignInManager<User> signInManager,UserManager<User> userManager,*/IBaseRepository baseRepository,IMediator mediator)
        {
            //_signinManager = signInManager;
            //_userManager = userManager;
            _baseRepository = baseRepository;
            _mediatr = mediator;
        }

        [HttpPost("Register")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Register new user", OperationId = "Register")]
        public async Task<IActionResult> Register(RegistrationCommand registrationCommand)
        {
          
            var res = await _mediatr.Send(registrationCommand);
            return Ok(res);
        }

        [HttpPost("Login")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Login in sustem", OperationId = "Login")]
        public async Task<IActionResult> Login(LoginCommand registrationCommand)
        {

            //var headers = Request.Headers.ToList();


            var res = await _mediatr.Send(registrationCommand);
            return Ok(res);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes ="Bearer")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(summary: "Get personal data", OperationId = "GetPersonalData")]
        public async Task<IActionResult> GetPersonalData()
        {
            GetPersonalDataQuery query = new GetPersonalDataQuery();
            query.UserId = UserId.ToString();
            var res = await _mediatr.Send(query);
            return Ok(res);
        }

    }
}
