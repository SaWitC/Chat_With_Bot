using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Features.Features.Commands.Messages.SendMessage
{
    public class SendMessageDTO
    {
        public string text { get; set; }
        public string avtroId { get; set; }
        public string ParentId { get; set; }
        public bool IsFromBot { get; set; } = false;
    }
}
