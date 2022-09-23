using BotServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hangfire;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BotServer.Application.Repositories;
using VkNet;
using VkNet.Model.RequestParams;
using Microsoft.Extensions.Configuration;
using VkNet.Model;
using BotServer.Application.HubsAbstraction;

namespace BotServer.Features.BackgroundJobs.Remind
{
    public class BGJobRemind : IBGJobRemind
    {
        private readonly UserManager<BotServer.Domain.Models.User> _userManager;
        private readonly IHubRepository _hubRepository;
        private readonly IConfiguration _configuration;
        //private readonly IChatHub _chatHub;

        public BGJobRemind(UserManager<BotServer.Domain.Models.User> userManager,
            IHubRepository hubRepository,
            IConfiguration configuration
            //IChatHub chatHub
            )
        {
            //_chatHub = chatHub;
            _userManager= userManager;
            _hubRepository= hubRepository;
            _configuration= configuration;
        }
        public async Task<bool> SetBgJobRemind(RemindModel remind)
        {
            if (!string.IsNullOrEmpty(remind.AvtorId))
            {
                var delay = DateTime.Now - remind.RemindAtTime;
                BackgroundJob.Schedule(() => SendMessage(remind), delay);
                return true;
            }
            return false;    
        }

        private async Task SendMessage(RemindModel remind)
        {
            var user = await _userManager.FindByIdAsync(remind.AvtorId);
            if (user != null)
            {
                if (user.SendToVk && !string.IsNullOrEmpty(user.VkId.ToString()))
                {
                    VkApi vkApi = new VkApi();
                    vkApi.Authorize(new ApiAuthParams()
                    {
                        AccessToken = _configuration["VkConfig:Token"]
                    });
                    await vkApi.Messages.SendAsync(new MessagesSendParams()
                    {
                        UserId = 404055010,
                        Message = "You loged in",
                        RandomId = Environment.TickCount
                    });
                }

                var activeConnections =await _hubRepository.GetAllActivConnectionsByUser(user.Id);

                foreach(var x in activeConnections)
                {
                    //await _chatHub.SendMessage(x.HubConnection, remind.RemindMessage);
                }
            }
        }
    }
}
