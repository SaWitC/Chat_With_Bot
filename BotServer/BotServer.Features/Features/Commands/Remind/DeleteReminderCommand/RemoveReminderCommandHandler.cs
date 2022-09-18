using BotServer.Application.Repositories;
using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Remind.DeleteReminder
{
    public class RemoveReminderCommandHandler:IRequestHandler<RemoveRemindCommand,bool>
    {
        private readonly IRemindRepository _remindRepostory;
        private readonly IBaseRepository _baseRepository;

        public RemoveReminderCommandHandler(IRemindRepository remindRepository,IBaseRepository baseRepository)
        {
            this._baseRepository = baseRepository;
            this._remindRepostory = remindRepository;
        }

        public async Task<bool> Handle(RemoveRemindCommand request, CancellationToken cancellationToken)
        {
            var res = await _baseRepository.FictiveRemove<RemindModel>(request.ReminderId);
            if (res != null)
            {
                await _baseRepository.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
