using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Chat.UpdateChatCommand
{
    public class UpdateChatCommand:IRequest<ChatModel>
    {
        public string oldModelId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}
