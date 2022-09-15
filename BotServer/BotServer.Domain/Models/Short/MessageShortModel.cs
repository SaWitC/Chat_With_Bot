using BotServer.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Domain.Models.Short
{
    public class MessageShortModel:IEntity
    {
        public string id { get; set; }
        public string text { get; set; }
        public string avtroId { get; set; }
        public bool IsFromBot { get; set; } = false;

        public DateTime Created { get; set; }
    }
}
