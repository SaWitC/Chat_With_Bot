using BotServer.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Chat.CreateChatCommand
{
    public class CreateChatCommand:IRequest<EntityEntry<ChatModel>>
    {
        public CreateChatDTO createChatDTO { get; set; }
    }
}
