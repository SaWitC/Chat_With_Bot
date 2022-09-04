using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Chat.DeleteChatCommand
{
    public class DeleteChatCommand:IRequest<bool>
    {
        public string id { get; set; }
    }
}
