using BotServer.Application.Repositories;
using BotServer.Data.Repositories;
using BotServer.Domain.ComuinicationModels;
using BotServer.Domain.Models;
using BotServer.SignalR_info.Hubs;
using Hangfire;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace BotServer.Features.BackgroundJobs.Remind
{
    public class BGJobRemind : IBGJobRemind
    {
        private readonly UserManager<BotServer.Domain.Models.User> _userManager;
        private readonly IHubRepository _hubRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BGJobRemind> _logger;
        private readonly IHubContext<HubForNotify> _notifyHub;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBaseRepository _baseRepository;
        private readonly IRemindRepository _remindRepository;

        public BGJobRemind(UserManager<BotServer.Domain.Models.User> userManager,
            IHubRepository hubRepository,
            IConfiguration configuration,
            IHubContext<HubForNotify> chatHub,
            ILogger<BGJobRemind> logger,
            IPublishEndpoint publishEndpoint,
            IBaseRepository baseRepository,
            IRemindRepository remindRepository
            )
        {
            _remindRepository = remindRepository;
            _publishEndpoint = publishEndpoint;
            _notifyHub = chatHub;
            _userManager = userManager;
            _hubRepository = hubRepository;
            _logger = logger;
            _configuration = configuration;
            _baseRepository = baseRepository;
        }
        public async Task<bool> SetBgJobRemind(RemindModel remind)
        {
            if (!string.IsNullOrEmpty(remind.AvtorId))
            {
                var delay = remind.RemindAtTime - DateTime.Now;
                BackgroundJob.Schedule(() => SendMessage(remind), delay);
                return true;
            }
            return false;
        }

        public async Task SendMessage(RemindModel remind)
        {
            var user = await _userManager.FindByIdAsync(remind.AvtorId);
            if (user != null)
            {
                if (user.SendToVk && !string.IsNullOrEmpty(user.VkId.ToString()))
                {
                    //VkApi vkApi = new VkApi();
                    //vkApi.Authorize(new ApiAuthParams()
                    //{
                    //    AccessToken = _configuration["VkConfig:Token"]
                    //});
                    //await vkApi.Messages.SendAsync(new MessagesSendParams()
                    //{
                    //    UserId = user.VkId,
                    //    Message = remind.RemindMessage,
                    //    RandomId = Environment.TickCount
                    //});
                    await _publishEndpoint.Publish<ComunicationMessage>(new ComunicationMessage { Text = remind.RemindMessage, VkId = user.VkId });
                    //await _publishEndpoint.Publish(new ComunicationMessage {Text=remind.RemindMessage ,VkId=user.VkId});
                }

                var activeConnections = await _hubRepository.GetAllActivConnectionsByUser(user.Id);
                if (activeConnections.Count() > 0)
                {
                    remind.IsDeleted = true;
                    await _baseRepository.FictiveRemove<RemindModel>(remind.id);
                    await _baseRepository.SaveChangesAsync();
                }

                foreach (var x in activeConnections)
                {
                    await _notifyHub.Clients.Client(x.HubConnection).SendAsync("Notify", remind.RemindMessage);
                }
            }
        }

        //public async Task SendOld(string connectionId, string userId)
        //{
        //    var expiredReminds = await _remindRepository.GetExpiredRemindsbyAvorId(userId);
        //    foreach (var x in expiredReminds)
        //    {
        //        try
        //        {
        //            await _notifyHub.Clients.Client(connectionId).SendAsync("Notify", x.RemindMessage);
        //            await _baseRepository.FictiveRemove<RemindModel>(x.id);
        //        }
        //        catch (Exception ex)
        //        {
        //            await _baseRepository.SaveChangesAsync();
        //            _logger.LogError($"the message send exception {ex.Message}");
        //        }
        //    }
        //    await _baseRepository.SaveChangesAsync();
        //    await _notifyHub.Clients.Client(connectionId).SendAsync("Notify", "tur");
        //}
    }
}
