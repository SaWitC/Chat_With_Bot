using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.ComuinicationModels
{
    public class ComunicationMessage
    {
        public string Text { get; set; }

        public long? VkId { get; set; }
    }
}
