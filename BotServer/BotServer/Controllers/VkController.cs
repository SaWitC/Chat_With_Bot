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

namespace BotServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VkController : ControllerBase
    {
        private readonly CustomClient _customClient;

        public Guid UserId => Guid.Parse(HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        public VkController(IConfiguration configuration,
            CustomClient customClient)
        {

            _customClient = customClient;
        }

        [Route("[action]/{message}")]
        
        [HttpGet]
        public async Task<IActionResult> TestMethod(string message)
        {
            var res = await _customClient.GetFileAsync(message);
            return Ok(res);
        }

    }
}
