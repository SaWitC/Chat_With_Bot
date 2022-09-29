using MediatR;

namespace BotServer.Features.Features.Commands.Remind.DeleteReminder
{
    public class RemoveRemindCommand : IRequest<bool>
    {
        public string AvtorId { get; set; }
        public string ReminderId { get; set; }

    }
}
