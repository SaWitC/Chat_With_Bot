using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Commands.Messages.SendMessage
{
    public class SendMessageCommand : IRequest<MessageModel>
    {
        public SendMessageDTO SendMessageDTO { get; set; }
    }
}
