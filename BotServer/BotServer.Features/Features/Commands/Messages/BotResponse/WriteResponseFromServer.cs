using BotServer.Domain.Models;
using MediatR;

namespace BotServer.Features.Features.Commands.Messages.BotResponse
{
    public class WriteResponseFromServer : IRequest<MessageModel>
    {
        public string MessageFromUser { get; set; }
        public string ChatId { get; set; }
    }
}
