using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Chat.CreateChatCommand
{
    public class CreateChatDTO
    {
        public string avtorId { get; set; }

        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}
