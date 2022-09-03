using BotServer.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.ChatCommands.CreateChatCommand
{
    public class CreateChatCommand:IRequest<EntityEntry<ChatModel>>
    {
        public CreateChatDTO createChatDTO { get; set; }
    }
}
