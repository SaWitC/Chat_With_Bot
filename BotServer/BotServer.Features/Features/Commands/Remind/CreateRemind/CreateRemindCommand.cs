using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Commands.Remind.CreateRemind
{
    public class CreateRemindCommand : IRequest<RemindModel>
    {
        public string RemindMessage { get; set; }
        public DateTime RemindAtTime { get; set; }
    }
}
