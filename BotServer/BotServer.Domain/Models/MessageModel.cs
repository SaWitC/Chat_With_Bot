using BotServer.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.Models
{
    public class MessageModel : IEntity
    {
        public string id { get; set; }

        public string text { get; set; }

        public string avtroId { get; set; }

        public string chatId { get; set; }

        public bool IsFromBot { get; set; } = false;

    }
}
