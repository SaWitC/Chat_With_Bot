using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace BotServer.Features.Features.Commands.Vk.VkAuthorization
{
    public class VkAIdSaveCommandHandler : IRequestHandler<VkAIdSaveCommand, long?>
    {
        private readonly UserManager<BotServer.Domain.Models.User> _userManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;


        public VkAIdSaveCommandHandler(
            UserManager<BotServer.Domain.Models.User> userManager,
            IAccountRepository accountRepository,
            IConfiguration configuration,
            IVkApi vkApi)
        {
            _userManager = userManager;
            _accountRepository = accountRepository;
            _configuration = configuration;
        }
        public async Task<long?> Handle(VkAIdSaveCommand request, CancellationToken cancellationToken)
        {
            var user =await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                VkApi api = new VkApi();
                string Token = _configuration["VkConfig:Token"];

                ulong AppId = ulong.Parse(_configuration["VkConfig:AppId"]);

                await api.AuthorizeAsync(new ApiAuthParams()
                {
                    ApplicationId = AppId,
                    Login = request.Email,
                    Password = request.Password,
                });

                if (api.UserId != null)
                {
                    user.VkId = api.UserId;
                    user.VkEmail = request.Email;
                   
                    await _userManager.UpdateAsync(user);

                    return api.UserId;
                }
            }
            return null;
        }
    }
}
