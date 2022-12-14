using BotServer.Application.Repositories;
using BotServer.Data.Repositories;
using BotServer.Domain.ComuinicationModels;
using BotServer.Domain.Models;
using BotServer.Features.Features.Commands.Vk.VkAuthorization;
using BotServer.Services.SwaggerComplettedRealisation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.Swagger;
using System.Security.Claims;
using BotServer.Data.Data;

namespace BotServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VkController : ControllerBase
    {
        private UserManager<User> _userManager;
        private readonly CustomClient _customClient;
        private readonly ChatRepository _chatRepository;

        public Guid UserId => Guid.Parse(HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        public VkController(IConfiguration configuration,
            CustomClient customClient,
            ChatRepository chatRepository,
            UserManager<User> userManager)
        {
            _chatRepository = chatRepository;
            _customClient = customClient;
            _userManager = userManager;
        }

        [Route("[action]/{message}")]
        
        [HttpGet]
        public async Task<IActionResult> TestMethod(string message)
        {
            var user =await _userManager.FindByNameAsync("user1");
            var x = _userManager.Users;
            //var y=_userManager.users
            // var res = await _customClient.GetFileAsync(message);
            //var res = _chatRepository.GetPageByAvtorId(avtorId:UserId.ToString(),0,5);
            return Ok(user);
        }

    }
}
